
namespace Models;
public class AppointmentServiceJoinTable
{

    public int AppointmentServiceId { get; set; } // Single-column primary key
    public int AppointmentId { get; set; } // Composite Key Part 1
    public int ServiceId { get; set; } // Composite Key Part 2

    // Optional attributes
    public decimal Cost { get; set; } // Cost of the service for this appointment

    // Navigation properties
    public Appointment Appointment { get; set; }
    public Service Service { get; set; }
}
