
namespace Models;
public class Appointment
{
    public int AppointmentId { get; set; } // Primary Key
    public int CustomerId { get; set; } // Foreign Key
    public int StylistId { get; set; } // Foreign Key
    public DateTime TimeOf { get; set; } // Date and time of the appointment
    public bool IsCancelled { get; set; } // Tracks cancellation status

    // Navigation properties
    public Customer Customer { get; set; }
    public Stylist Stylist { get; set; }
    public List<AppointmentServiceJoinTable> AppointmentServices { get; set; }
}
