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



//public class CreateAppointmentDTO
// {
//     public int CustomerId { get; set; }
//     public int StylistId { get; set; }
//     public DateTime TimeOf { get; set; }
//     public List<int>? ServiceIds { get; set; } // IDs of services for this appointment
// }





app.MapPost("api/customers", async (HilaryDbContext db, IMapper mapper, CreateCustomerDTO createCustomerDTO) =>
{
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
            { Id = appointment.Stylist.Id,
              Name = appointment.Stylist.Name,
              IsActive = appointment.Stylist.IsActive
            },
            Customer = new {
                Id = appointment.CustomerId,
                Name = appointment.Customer.Name,
            },
            Services = appointment.AppointmentServiceJoinList
            .Select(asJL => new 
            { ServiceId = asJL.Service.ServiceId,
                 Name = asJL.Service.Name,
                 Price = asJL.Service.Price,
                DurationMinutes = asJL.Service.DurationMinutes
            }).ToList()
        }),
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



    // var customer = mapper.Map<Customer>(createCustomerDTO);
    // db.Customers.Add(customer);
    // await db.SaveChangesAsync();
    // var customerDTO = mapper.Map<CustomerDTO>(customer); 
    // customerDTO.AppointmentIds = new List<int>();
    // return Results.Created($"/api/customers/{customer.CustomerId}", customerDTO);
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



app.Run();