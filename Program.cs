using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models; // Namespace for your models
using Mapper; // Namespace for your MappingProfile class
using Models.DTOs;
using Mapper;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5173") // Replace with your frontend URL if needed
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

builder.Logging.AddConsole().AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Information);


// Configure EF Core with PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Handles legacy timestamp behavior
builder.Services.AddNpgsql<HilaryDbContext>(builder.Configuration["HilaryProjectDbConnectionString"]); // Replace with your DbContext class

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Add Swagger/OpenAPI (for API documentation and testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS Policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add endpoints (replace placeholder routes as needed)
app.MapGet("/", () => "Hillary's Hair Care API is running!");


app.MapPost("api/customers", async (HilaryDbContext db, IMapper mapper, CreateCustomerDTO createCustomerDTO) =>
{

      if (string.IsNullOrWhiteSpace(createCustomerDTO.Name))
        return Results.BadRequest(new { Message = "Customer name cannot be empty." });
    // Map the incoming DTO to a Customer entity
    var newCustomer = mapper.Map<Customer>(createCustomerDTO);

    // Add the new customer to the database
    await db.Customers.AddAsync(newCustomer);
    await db.SaveChangesAsync();

    // Map the created customer back to a CustomerDTO
    var customerDTO = mapper.Map<CustomerDTO>(newCustomer);

    return Results.Created($"api/customers/{newCustomer.CustomerId}", customerDTO);
});


app.MapGet("api/customers", async (HilaryDbContext db, IMapper mapper) =>
{

    // var services = db.Services
    // .Include(s => s.AppointmentServiceJoinList)
    // .ThenInclude(asJoinTable => asJoinTable.Appointment) //this is key we will need the join tables to also have appointments later
    // .Include(s => s.StylistServiceJoinList)
    // .ThenInclude(stylistService => stylistService.Stylist)
    
 var customers = await db.Customers
    .Include(c => c.Appointments) // Include the appointments of each customer
        .ThenInclude(a => a.Stylist)
        .ThenInclude(a => a.StylistServiceJoinList) // Include the stylist for each appointment
    .Include(c => c.Appointments) 
        .ThenInclude(a => a.Customer) // Include the customer (if needed redundantly within appointments)
    .Include(c => c.Appointments)
        .ThenInclude(a => a.AppointmentServiceJoinList) // Include the join table for services
        .ThenInclude(asj => asj.Service)
        .Select(customer => new
    {
        CustomerId = customer.CustomerId,
        Name = customer.Name,
        Appointments = customer.Appointments.Select(appointment => new
        {
            AppointmentId = appointment.AppointmentId,
            TimeOf = appointment.TimeOf,
            IsCancelled = appointment.IsCancelled,
            Stylist = new
            {
                Id = appointment.Stylist.Id,
                Name = appointment.Stylist.Name,
                IsActive = appointment.Stylist.IsActive
            },
            Customer = new
            {
                Id = appointment.CustomerId,
                Name = appointment.Customer.Name
            },
            Services = appointment.AppointmentServiceJoinList
                .Select(asj => new
                {
                    ServiceId = asj.Service.ServiceId,
                    Name = asj.Service.Name,
                    Price = asj.Service.Price,
                    DurationMinutes = asj.Service.DurationMinutes
                }).ToList()
        }).ToList(),
        Services = customer.Appointments.SelectMany(eachAppt => 
        eachAppt.AppointmentServiceJoinList.Select( asJL => new {
             ServiceId = asJL.Service.ServiceId,
             Name = asJL.Service.Name,
             Price = asJL.Service.Price,
             DurationMinutes = asJL.Service.DurationMinutes
        })).ToList(),
        Stylist = customer.Appointments.Select(appt => new {
            StylistId = appt.Stylist.Id,
            Name = appt.Stylist.Name,
            IsActive = appt.Stylist.IsActive
        }).ToList()

    }) // Include the actual service from the join table
    .ToListAsync();


return Results.Ok(customers);

});


app.MapGet("api/appointments", async (HilaryDbContext db) =>
{
    var appointments = await db.Appointments
        .Include(a => a.Customer) // Include Customer details
        .Include(a => a.Stylist) // Include Stylist details
        .Include(a => a.AppointmentServiceJoinList) // Include Join Table for Services
            .ThenInclude(asj => asj.Service) // Include Services from the Join Table
        .Select(indAppt => new
        {
            AppointmentId = indAppt.AppointmentId,
            CustomerId = indAppt.CustomerId,
            CustomerName = indAppt.Customer.Name,
            StylistName = indAppt.Stylist.Name,
            TimeOf = indAppt.TimeOf,
            IsCancelled = indAppt.IsCancelled,
            Services = indAppt.AppointmentServiceJoinList.Select(asJoin => new
            {
                ServiceId = asJoin.ServiceId,
                Name = asJoin.Service.Name,
                Price = asJoin.Service.Price,
                DurationMinutes = asJoin.Service.DurationMinutes
            }).ToList(),
            Customer = new
            {
                CustomerId = indAppt.Customer.CustomerId,
                Name = indAppt.Customer.Name,
                AppointmentIds = indAppt.Customer.Appointments.Select(appt => appt.AppointmentId).ToList()
            },
            Stylist = new
            {
                StylistId = indAppt.Stylist.Id,
                Name = indAppt.Stylist.Name,
                IsActive = indAppt.Stylist.IsActive,
                ServiceIds = indAppt.Stylist.StylistServiceJoinList
                    .Select(ssj => ssj.ServiceId)
                    .ToList()
            }
        }).ToListAsync();

    return Results.Ok(appointments); // Return all appointments
});

app.MapGet("api/appointments/{id}", async (HilaryDbContext db, IMapper mapper, int id) =>
{
    var appointment = await db.Appointments
        .Include(a => a.Customer)
        .Include(a => a.Stylist)
        .Include(a => a.AppointmentServiceJoinList)
        .ThenInclude(asj => asj.Service)
        .FirstOrDefaultAsync(a => a.AppointmentId == id);

    if (appointment == null) return Results.NotFound();

    // var appointmentDTO = mapper.Map<AppointmentDTO>(appointment); // Map to DTO

    //YOU CANT RUN SELECT ON APPOINTMENT THIS IS WRONG 

    var appointmentDTO = db.Appointments.
    Include(a => a.Customer)
    .Include(a => a.Stylist)
    .Select(indAppt => new AppointmentDTO
    { AppointmentId = indAppt.AppointmentId,
      CustomerId = indAppt.CustomerId,
      CustomerName = indAppt.Customer.Name,
      StylistName = indAppt.Stylist.Name,
      TimeOf = indAppt.TimeOf,
      IsCancelled = indAppt.IsCancelled,
      ServiceIds = indAppt.AppointmentServiceJoinList.Where(eachAppt => eachAppt.AppointmentId == indAppt.AppointmentId)
                    .Select(asJoin => asJoin.ServiceId).ToList(),
      CustomerDTO = new CustomerDTO {
        CustomerId = indAppt.CustomerId,
        Name = indAppt.Customer.Name,
        AppointmentIds = indAppt.Customer.Appointments.Select(appt => appt.AppointmentId).ToList()
      },
      StylistDTO = new StylistDTO
        {
            StylistId = appointment.Stylist.Id,
            Name = appointment.Stylist.Name,
            IsActive = appointment.Stylist.IsActive,
            ServiceIds = appointment.Stylist.StylistServiceJoinList
                .Select(ssj => ssj.ServiceId)
                .ToList()
            }
    });
    return Results.Ok(appointmentDTO);
});


app.MapPost("api/appointments", async (HilaryDbContext db, CreateAppointmentDTO newAppointmentDTO) =>
{
     Console.WriteLine($"Received Appointment DTO: CustomerId={newAppointmentDTO.CustomerId}, StylistId={newAppointmentDTO.StylistId}, TimeOf={newAppointmentDTO.TimeOf}, ServiceIds={string.Join(",", newAppointmentDTO.ServiceIds ?? new List<int>())}");

    // Validate the input
    if (newAppointmentDTO.CustomerId <= 0 || newAppointmentDTO.StylistId <= 0 || newAppointmentDTO.ServiceIds == null || !newAppointmentDTO.ServiceIds.Any())
    {
        return Results.BadRequest("Invalid appointment data.");
    }

    if (!db.Customers.Any(c => c.CustomerId == newAppointmentDTO.CustomerId))
{
    return Results.BadRequest($"Invalid CustomerId: {newAppointmentDTO.CustomerId}");
}

        // Ensure the Customer exists
    var customer = await db.Customers.FindAsync(newAppointmentDTO.CustomerId);
    if (customer == null)
    {
        return Results.NotFound($"Customer with ID {newAppointmentDTO.CustomerId} not found.");
    }
    db.Entry(customer).State = EntityState.Unchanged;

    var stylist = await db.Stylists.Include(s => s.Appointments).FirstOrDefaultAsync(s => s.Id == newAppointmentDTO.StylistId);

    if (customer == null || stylist == null)
    {
        return Results.NotFound("Customer or stylist not found.");
    }

    // Create the Appointment
    var newAppointment = new Appointment
    {
        CustomerId = newAppointmentDTO.CustomerId,
        StylistId = newAppointmentDTO.StylistId,
        TimeOf = newAppointmentDTO.TimeOf ?? DateTime.Now,
        IsCancelled = false,
        // Customer = customer,Instead, rely on EF Core's ability to link the Customer through the CustomerId foreign key.
        // Remove the Customer = customer assignment and only assign CustomerId.
        Stylist = stylist,
        AppointmentServiceJoinList = new List<AppointmentServiceJoinTable>()
    };

    db.Appointments.Add(newAppointment);
       //we need this to generate our appointmentID for the appointmentserviceJointable later 

    // customer.Appointments.Add(newAppointment);
 //IMPORTANT DONT NEED THIS TO ENSURE ensure the Appointment is correctly saved or linked to the Customer
 // because the relationship is already established via the CustomerId and Customer navigation property in the newAppointment object

    await db.SaveChangesAsync(); // Now newAppointment.AppointmentId is set


    foreach (int serviceId in newAppointmentDTO.ServiceIds)
    {
        var service = db.Services.FirstOrDefault(s => s.ServiceId == serviceId);
        if (service == null)
        {
            return Results.NotFound("service not found for the list of ids passed");
        }
        AppointmentServiceJoinTable asJT = new AppointmentServiceJoinTable
        {
            AppointmentId = newAppointment.AppointmentId,
            ServiceId = serviceId,
            Cost = service.Price,
            // Appointment = newAppointment,
            // Service = service
        };
        db.AppointmentServices.Add(asJT);
        await db.SaveChangesAsync();

        service.AppointmentServiceJoinList.Add(asJT);
        await db.SaveChangesAsync();
    }
    // Save the join table entries
    await db.SaveChangesAsync();
    return Results.Created($"/api/appointments/{newAppointment.AppointmentId}", newAppointmentDTO);

});


app.MapGet("api/services", async (HilaryDbContext db, IMapper mapper) =>
{
    var stylist = await db.Stylists
    .Include(stylist => stylist.Appointments)
    .Include(stylist => stylist.StylistServiceJoinList).
    ToListAsync();

    var services = db.Services
    .Include(s => s.AppointmentServiceJoinList)
    .ThenInclude(asJoinTable => asJoinTable.Appointment) //this is key we will need the join tables to also have appointments later
    .Include(s => s.StylistServiceJoinList)
    .ThenInclude(stylistService => stylistService.Stylist)
    .Select(service => new
    {
        service.ServiceId,
        service.Name,
        service.Price,
        service.DurationMinutes,
        Stylists = service.StylistServiceJoinList
        .Select(ssj => new
    {
        Id = ssj.Stylist.Id,
        Name = ssj.Stylist.Name,
        IsActive = ssj.Stylist.IsActive
    }) .ToList(),
    Appointments = service.AppointmentServiceJoinList
    .Select(asj => new 
    { AppointmentId = asj.AppointmentId,
      CustomerId = asj.Appointment.CustomerId,
      CustomerName = asj.Appointment.Customer.Name,
      StylistId = asj.Appointment.StylistId,
      StylistName = asj.Appointment.Stylist.Name,
      TimeOf = asj.Appointment.TimeOf,
      IsCancelled = asj.Appointment.IsCancelled,
      ServiceIds = asj.Appointment.AppointmentServiceJoinList // now we us that asJoinTable.Appointment navigation prop from earlier
      .Select(asList => asList.ServiceId).ToList()

    }).ToList()
   
    })
    .ToList();
    return Results.Ok(services);

// this is inefficient they said so well do a better way above 
    // var services = db.Services
    // .Select(service => new
    // {
    //     service.ServiceId,
    //     service.Name,
    //     service.Price,
    //     service.DurationMinutes,
    //     Stylists = service.StylistServiceJoinList.
    //     Where( s => s.ServiceId == service.ServiceId)
    //     .Select(s => s.StylistId)
    //     .Select(stylistIds => stylist.FirstOrDefault(eachStylist => eachStylist.Id == stylistIds))
    //     .ToList()
    // });

    // var services = await db.Services.ToListAsync();
    // var serviceDTOs = mapper.Map<List<ServiceDTO>>(services); // Map to DTOs
    // return Results.Ok(serviceDTOs);
});

app.MapGet("api/stylists", async (HilaryDbContext db, IMapper mapper) =>
{
    var stylists = await db.Stylists
        .Include(s => s.Appointments)
            .ThenInclude(a => a.Customer) // Include Customer details for each appointment
        .Include(s => s.StylistServiceJoinList)
            .ThenInclude(ssj => ssj.Service) // Include Services for each Stylist
        .Select(s => new
        {
            StylistId = s.Id, // Stylist ID
            Name = s.Name, // Stylist Name
            IsActive = s.IsActive, // Stylist active status
            Appointments = s.Appointments.Select(a => new
            {
                AppointmentId = a.AppointmentId,
                Customer = new
                {
                    CustomerId = a.CustomerId,
                    Name = a.Customer.Name
                },
                TimeOf = a.TimeOf,
                IsCancelled = a.IsCancelled
            }).ToList(),
            Services = s.StylistServiceJoinList.Select(ssj => new
            {
                ServiceId = ssj.Service.ServiceId,
                Name = ssj.Service.Name,
                Price = ssj.Service.Price,
                DurationMinutes = ssj.Service.DurationMinutes
            }).ToList()
        })
        .ToListAsync();

    return Results.Ok(stylists);
});


app.MapGet("api/stylists/{id}", async (HilaryDbContext db, IMapper mapper, int id) =>
{
    var stylist = await db.Stylists
        .Include(s => s.StylistServiceJoinList)
        .ThenInclude(ssj => ssj.Service)
        .FirstOrDefaultAsync(s => s.Id== id);

    if (stylist == null) return Results.NotFound();

    var stylistDTO = mapper.Map<StylistDTO>(stylist); // Map to DTO
    return Results.Ok(stylistDTO);
});


app.MapPut("api/stylists/{id}", async (HilaryDbContext db, IMapper mapper, int id, UpdateStylistDTO updateStylistDTO) =>
{
    var stylist = await db.Stylists.FirstOrDefaultAsync(s => s.Id == id);

    if (stylist == null) return Results.NotFound();

    // Map updates from DTO
    mapper.Map(updateStylistDTO, stylist);

    await db.SaveChangesAsync();

    var stylistDTO = mapper.Map<StylistDTO>(stylist); // Return updated DTO
    return Results.Ok(stylistDTO);
});

app.MapGet("api/debug-appointment-services", async (HilaryDbContext db) =>
{
    var existingData = await db.AppointmentServices.ToListAsync();
    Console.WriteLine("=== Existing Data in AppointmentServices ===");
    foreach (var entry in existingData)
    {
        Console.WriteLine($"AppointmentServiceId: {entry.AppointmentServiceId}, AppointmentId: {entry.AppointmentId}, ServiceId: {entry.ServiceId}, Cost: {entry.Cost}");
    }
    return Results.Ok(existingData);
});



/// these 2 commented out did work btw if you need them later 
// app.MapGet("api/customers/{customerId}/appointments", async (int customerId, HilaryDbContext db) =>
// {
//     var appointmentIds = await db.Appointments
//         .Where(a => a.CustomerId == customerId)
//         .Select(a => a.AppointmentId)
//         .ToListAsync();

//     return Results.Ok(appointmentIds);
// });


// app.MapGet("api/appointments/{appointmentId}/services", async (int appointmentId, HilaryDbContext db) =>
// {
//     var appointmentWithServices = await db.Appointments
//         .Where(a => a.AppointmentId == appointmentId)
//         .Select(a => new
//         {
//             AppointmentId = a.AppointmentId,
//             Services = a.AppointmentServiceJoinList
//                 .Select(asj => new
//                 {
//                     ServiceId = asj.Service.ServiceId,
//                     Name = asj.Service.Name,
//                     Price = asj.Service.Price,
//                     DurationMinutes = asj.Service.DurationMinutes
//                 })
//                 .ToList()
//         })
//         .FirstOrDefaultAsync();

//     if (appointmentWithServices == null)
//     {
//         return Results.NotFound(new { Message = $"No services found for AppointmentId {appointmentId}" });
//     }

//     return Results.Ok(appointmentWithServices);
// });

app.MapGet("api/customers/{customerId}/appointmentAndService", async (int customerId, HilaryDbContext db) =>
{
    var customerAppointmentsWithServices = await db.Appointments
        .Where(a => a.CustomerId == customerId)
        .Select(a => new
        {
            AppointmentId = a.AppointmentId,
            TimeOf = a.TimeOf,
            IsCancelled = a.IsCancelled,
            Services = a.AppointmentServiceJoinList
                .Select(asj => new
                {
                    ServiceId = asj.Service.ServiceId,
                    Name = asj.Service.Name,
                    Price = asj.Service.Price,
                    DurationMinutes = asj.Service.DurationMinutes
                })
                .ToList()
        })
        .ToListAsync();

    if (customerAppointmentsWithServices.Count == 0)
    {
        return Results.NotFound(new { Message = $"No appointments or services found for CustomerId {customerId}" });
    }

    return Results.Ok(customerAppointmentsWithServices);
});


app.MapPut("api/stylists/{id}/deactivate", async (HilaryDbContext db, int id) =>
{
    var stylist = await db.Stylists.FirstOrDefaultAsync(s => s.Id == id);

    if (stylist == null)
        return Results.NotFound(new { Message = $"Stylist with ID {id} not found." });

    stylist.IsActive = false; // Deactivate stylist
    await db.SaveChangesAsync();

    return Results.Ok(new { Message = $"Stylist with ID {id} has been deactivated." });
});




app.MapPut("api/appointments/{appointmentId}", async (HilaryDbContext db, int appointmentId, UpdateAppointmentDTO updatedAppointmentDTO) =>
{
    Console.WriteLine($"Updating Appointment ID {appointmentId} with new data.");

    // Find the existing appointment
    var appointment = await db.Appointments
        .Include(a => a.AppointmentServiceJoinList)
        .ThenInclude(asj => asj.Service)
        .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

    if (appointment == null)
    {
        return Results.NotFound($"Appointment with ID {appointmentId} not found.");
    }
  // Update appointment time if necessary
    if (updatedAppointmentDTO.TimeOf.HasValue && updatedAppointmentDTO.TimeOf != appointment.TimeOf)
    {
        appointment.TimeOf = updatedAppointmentDTO.TimeOf.Value;
    }

    // Update cancellation status if necessary
    if (updatedAppointmentDTO.IsCancelled != appointment.IsCancelled)
    {
        appointment.IsCancelled = updatedAppointmentDTO.IsCancelled ?? appointment.IsCancelled;
        //how to get around fact it is public bool? IsCancelled { get; set; }
    }

        // Update stylist if necessary
    if (updatedAppointmentDTO.StylistId != appointment.StylistId)
    {
        var stylist = await db.Stylists.FirstOrDefaultAsync(s => s.Id == updatedAppointmentDTO.StylistId);
        if (stylist == null)
        {
            return Results.NotFound($"Stylist with ID {updatedAppointmentDTO.StylistId} not found.");
        }
        appointment.StylistId = updatedAppointmentDTO.StylistId;
        appointment.Stylist = stylist;
    }


// Step 1: Retrieve current services for the appointment
var currentServiceIds = appointment.AppointmentServiceJoinList
    .Select(asj => asj.ServiceId)
    .ToList();

// Step 2: Compare current and updated service IDs
var updatedServiceIds = updatedAppointmentDTO.UpdatedServiceIds ?? new List<int>();

var servicesToRemove = currentServiceIds.Except(updatedServiceIds).ToList(); // Services no longer needed
var servicesToAdd = updatedServiceIds.Except(currentServiceIds).ToList();   // New services to add

//ex 
// var currentServiceIds = new List<int> { 1, 2, 3, 4 };
// var updatedServiceIds = new List<int> { 3, 4, 5, 6 };
// var servicesToRemove = currentServiceIds.Except(updatedServiceIds).ToList();
// currentServiceIds: { 1, 2, 3, 4 }
// updatedServiceIds: { 3, 4, 5, 6 }
// Result (servicesToRemove): { 1, 2 }

//so now directly remove then from the appointment services join table 
db.AppointmentServices.RemoveRange(
    db.AppointmentServices.Where(asJ => servicesToRemove.Contains(asJ.ServiceId)));

await db.SaveChangesAsync();
    //now directly add the updated Services to this join table 

    var newJoinTableEntries = servicesToAdd
                    .Select(serviceId => {
                     var service = db.Services.FirstOrDefault(s => s.ServiceId == serviceId);
                      if (service == null) {
                          throw new InvalidOperationException($"Service with ID {serviceId} not found."); // Short-circuit the process
                        }
                        return new AppointmentServiceJoinTable {
                            AppointmentId = appointmentId,
                            ServiceId = serviceId,
                            Cost = service.Price,
                            Appointment = db.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId),
                            Service = service
                        };
                              }).ToList();

    db.AppointmentServices.AddRange(newJoinTableEntries);
await db.SaveChangesAsync();

var updatedAppointment = await db.Appointments
    .Include(a => a.AppointmentServiceJoinList)
        .ThenInclude(asj => asj.Service)
    .Include(a => a.Stylist)
    .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

if (updatedAppointment == null)
{
    return Results.NotFound($"Updated appointment with ID {appointmentId} not found.");
}

var updatedAppointmentDTOReturn = new 
{
    AppointmentId = updatedAppointment.AppointmentId,
    CustomerId = updatedAppointment.CustomerId,
    StylistId = updatedAppointment.StylistId,
    TimeOf = updatedAppointment.TimeOf,
    IsCancelled = updatedAppointment.IsCancelled,
    Services = updatedAppointment.AppointmentServiceJoinList.Select(asj => new ServiceDTO
    {
        ServiceId = asj.Service.ServiceId,
        Name = asj.Service.Name,
        Price = asj.Service.Price,
        DurationMinutes = asj.Service.DurationMinutes
    }).ToList()
};


return Results.Ok(updatedAppointmentDTOReturn);
});



// public class Appointment {
//     public int AppointmentId { get; set; } // Primary Key
//     public int CustomerId { get; set; } // Foreign Key
//     public int StylistId { get; set; } // Foreign Key
//     public DateTime TimeOf { get; set; } // Date and time of the appointment
//     public bool IsCancelled { get; set; } // Tracks cancellation status
   // Navigation properties
//     public Customer Customer { get; set; }
//     public Stylist Stylist { get; set; }
//     public List<AppointmentServiceJoinTable> AppointmentServiceJoinList { get; set; }
// }

//     public class UpdateAppointmentDTO {
//         public int AppointmentId { get; set; }
//         public int StylistId { get; set; } // Include stylist ID for potential updates
//         public DateTime? TimeOf { get; set; } // Allow updating the appointment time
//         public bool? IsCancelled { get; set; } // Allow toggling the cancellation status
//         public List<int>? UpdatedServiceIds { get; set; } // Updated list of service IDs
//     }

// public class AppointmentServiceJoinTable{

//     public int AppointmentServiceId { get; set; } // Single-column primary key
//     public int AppointmentId { get; set; } // Composite Key Part 1
//     public int ServiceId { get; set; } // Composite Key Part 2
    // Optional attributes
//     public decimal Cost { get; set; } // Cost of the service for this appointment
  // Navigation properties
//     public Appointment Appointment { get; set; }
//     public Service Service { get; set; }
// }






app.Run();