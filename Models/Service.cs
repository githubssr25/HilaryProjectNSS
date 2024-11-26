namespace Models;


public class Service
{
    public int ServiceId { get; set; } // Primary Key
    public string Name { get; set; } // Name of the service
    public decimal Price { get; set; } // Price of the service
    public int DurationMinutes { get; set; } // Duration of the service in minutes

    // Navigation properties
    public List<AppointmentServiceJoinTable> AppointmentServices { get; set; }
    public List<StylistServiceJoinTable> StylistServices { get; set; }
}
