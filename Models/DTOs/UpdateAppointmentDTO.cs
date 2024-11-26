namespace Models.DTOs;

public class UpdateAppointmentDTO
{
    public int AppointmentId { get; set; }
    public bool IsCancelled { get; set; }
    public List<int> UpdatedServiceIds { get; set; } // IDs of updated services
}
