namespace Models.DTOs;

public class AppointmentDTO
{
    public int AppointmentId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int StylistId { get; set; }
    public string StylistName { get; set; }
    public DateTime TimeOf { get; set; }
    public bool IsCancelled { get; set; }
    public List<int> ServiceIds { get; set; } // IDs of services for this appointment
}
