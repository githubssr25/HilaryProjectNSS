using Microsoft.EntityFrameworkCore;
using Models;
public class HilaryDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Stylist> Stylists { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
   public DbSet<AppointmentServiceJoinTable> AppointmentServices { get; set; }

    public DbSet<StylistServiceJoinTable> StylistServices { get; set; }

    public HilaryDbContext(DbContextOptions<HilaryDbContext> options) : base(options)
    {
    }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
{


modelBuilder.Entity<AppointmentServiceJoinTable>()
.HasKey(asj => asj.AppointmentServiceId);// Specify primary key

modelBuilder.Entity<StylistServiceJoinTable>()
.HasKey(asj => asj.StylistServiceId);// Specify primary key

    // Seed data for Customers
modelBuilder.Entity<Customer>().HasData(
    new Customer { CustomerId = 1, Name = "Alice Johnson" },
    new Customer { CustomerId = 2, Name = "Bob Smith" },
    new Customer { CustomerId = 3, Name = "Charlie Brown" },
    new Customer { CustomerId = 4, Name = "Diane Evans" },
    new Customer { CustomerId = 5, Name = "Ethan Parker" },
    new Customer { CustomerId = 6, Name = "Fiona Grey" },
    new Customer { CustomerId = 7, Name = "Grace White" },
    new Customer { CustomerId = 8, Name = "Henry Green" },
    new Customer { CustomerId = 9, Name = "Isla Blue" }
);


    // Seed data for Stylists
// Seed data for Stylists
modelBuilder.Entity<Stylist>().HasData(
    new Stylist { Id = 1, Name = "Sophia Carter", IsActive = true },
    new Stylist { Id = 2, Name = "Oliver Davis", IsActive = true },
    new Stylist { Id = 3, Name = "Liam Taylor", IsActive = false },
    new Stylist { Id = 4, Name = "Emma Wilson", IsActive = true },
    new Stylist { Id = 5, Name = "Ava Moore", IsActive = true },
    new Stylist { Id = 6, Name = "Mason Harris", IsActive = false },
    new Stylist { Id = 7, Name = "Lucas Walker", IsActive = true },
    new Stylist { Id = 8, Name = "Mia Hall", IsActive = true }
);


    // Seed data for Services
   modelBuilder.Entity<Service>().HasData(
    new Service { ServiceId = 1, Name = "Haircut", Price = 20.00M, DurationMinutes = 60 },
    new Service { ServiceId = 2, Name = "Hair Coloring", Price = 50.00M, DurationMinutes = 120 },
    new Service { ServiceId = 3, Name = "Beard Trim", Price = 15.00M, DurationMinutes = 30 },
    new Service { ServiceId = 4, Name = "Perm", Price = 70.00M, DurationMinutes = 150 },
    new Service { ServiceId = 5, Name = "Hair Treatment", Price = 30.00M, DurationMinutes = 90 }
);


    // Seed data for Appointments
modelBuilder.Entity<Appointment>().HasData(
    new Appointment { AppointmentId = 1, CustomerId = 1, StylistId = 1, TimeOf = new DateTime(2024, 12, 1, 10, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 2, CustomerId = 2, StylistId = 2, TimeOf = new DateTime(2024, 12, 1, 11, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 3, CustomerId = 3, StylistId = 4, TimeOf = new DateTime(2024, 12, 1, 12, 0, 0), IsCancelled = true }, // Cancelled
    new Appointment { AppointmentId = 4, CustomerId = 4, StylistId = 5, TimeOf = new DateTime(2024, 12, 2, 10, 0, 0), IsCancelled = true }, // Cancelled
    new Appointment { AppointmentId = 5, CustomerId = 5, StylistId = 6, TimeOf = new DateTime(2024, 12, 2, 11, 0, 0), IsCancelled = true }, // Cancelled
    new Appointment { AppointmentId = 6, CustomerId = 6, StylistId = 7, TimeOf = new DateTime(2024, 12, 2, 12, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 7, CustomerId = 7, StylistId = 8, TimeOf = new DateTime(2024, 12, 3, 10, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 8, CustomerId = 8, StylistId = 1, TimeOf = new DateTime(2024, 12, 3, 11, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 9, CustomerId = 9, StylistId = 2, TimeOf = new DateTime(2024, 12, 3, 12, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 10, CustomerId = 1, StylistId = 3, TimeOf = new DateTime(2024, 12, 4, 10, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 11, CustomerId = 2, StylistId = 4, TimeOf = new DateTime(2024, 12, 4, 11, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 12, CustomerId = 3, StylistId = 5, TimeOf = new DateTime(2024, 12, 4, 12, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 13, CustomerId = 4, StylistId = 7, TimeOf = new DateTime(2024, 12, 5, 10, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 14, CustomerId = 5, StylistId = 8, TimeOf = new DateTime(2024, 12, 5, 11, 0, 0), IsCancelled = false },
    new Appointment { AppointmentId = 15, CustomerId = 6, StylistId = 1, TimeOf = new DateTime(2024, 12, 5, 12, 0, 0), IsCancelled = false }
);

// modelBuilder.Entity<AppointmentServiceJoinTable>().HasData(
//     new AppointmentServiceJoinTable { AppointmentServiceId = 1, AppointmentId = 1, ServiceId = 1, Cost = 20.00M }, // Haircut for Appointment 1
//     new AppointmentServiceJoinTable { AppointmentServiceId = 2, AppointmentId = 1, ServiceId = 2, Cost = 50.00M }, // Hair Coloring for Appointment 1
//     new AppointmentServiceJoinTable { AppointmentServiceId = 3, AppointmentId = 2, ServiceId = 3, Cost = 15.00M }, // Beard Trim for Appointment 2
//     new AppointmentServiceJoinTable { AppointmentServiceId = 4, AppointmentId = 2, ServiceId = 4, Cost = 70.00M }, // Perm for Appointment 2
//     new AppointmentServiceJoinTable { AppointmentServiceId = 5, AppointmentId = 3, ServiceId = 5, Cost = 30.00M }  // Hair Treatment for Appointment 3
// );

// modelBuilder.Entity<AppointmentServiceJoinTable>().HasData(
//     new AppointmentServiceJoinTable { AppointmentServiceId = 1, AppointmentId = 1, ServiceId = 1, Cost = 20.00M }, // Haircut for Appointment 1
//     new AppointmentServiceJoinTable { AppointmentServiceId = 2, AppointmentId = 1, ServiceId = 2, Cost = 50.00M }, // Hair Coloring for Appointment 1
//     new AppointmentServiceJoinTable { AppointmentServiceId = 3, AppointmentId = 2, ServiceId = 3, Cost = 15.00M }, // Beard Trim for Appointment 2
//     new AppointmentServiceJoinTable { AppointmentServiceId = 4, AppointmentId = 2, ServiceId = 4, Cost = 70.00M }, // Perm for Appointment 2
//     new AppointmentServiceJoinTable { AppointmentServiceId = 5, AppointmentId = 3, ServiceId = 5, Cost = 30.00M }  // Hair Treatment for Appointment 3
// );




modelBuilder.Entity<Appointment>()
.HasOne(a => a.Customer)
.WithMany(c => c.Appointments)
.HasForeignKey(a => a.CustomerId);

modelBuilder.Entity<Appointment>()
.HasOne(a => a.Stylist)
.WithMany(s => s.Appointments)
.HasForeignKey(a => a.StylistId);

modelBuilder.Entity<AppointmentServiceJoinTable>()
.HasOne(asj => asj.Appointment)
.WithMany(a => a.AppointmentServiceJoinList)
//Appointment can have many AppointmentServiceJoinTable entries.
//The Appointment entity has a navigation property called 
//AppointmentServiceJoinList (a List<AppointmentServiceJoinTable>), which lets us access all the related join table entries for a given Appointment.
.HasForeignKey(asj => asj.AppointmentId);
//EF Core that the AppointmentId column in AppointmentServiceJoinTable is the foreign key linking to the Appointment entity.

 // Service relationship
    modelBuilder.Entity<AppointmentServiceJoinTable>()
        .HasOne(asj => asj.Service)
        .WithMany(s => s.AppointmentServiceJoinList)
        .HasForeignKey(asj => asj.ServiceId);





modelBuilder.Entity<StylistServiceJoinTable>()
    .HasOne(ssj => ssj.Stylist)
    .WithMany(s => s.StylistServiceJoinList)
    .HasForeignKey(ssj => ssj.StylistId);

modelBuilder.Entity<StylistServiceJoinTable>()
    .HasOne(ssj => ssj.Service)
    .WithMany(s => s.StylistServiceJoinList)
    .HasForeignKey(ssj => ssj.ServiceId);








}


}