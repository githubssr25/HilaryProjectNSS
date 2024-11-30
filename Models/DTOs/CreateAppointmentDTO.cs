namespace Models.DTOs;

public class CreateAppointmentDTO
{
    public int CustomerId { get; set; }
    public int StylistId { get; set; }
    public DateTime? TimeOf { get; set; }
    public List<int>? ServiceIds { get; set; } // IDs of services for this appointment
}
