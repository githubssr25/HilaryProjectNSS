namespace Models.DTOs;

public class StylistDTO
{
    public int StylistId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public List<int> ServiceIds { get; set; } // Services the stylist specializes in
    public List<int> AppointmentIds { get; set; } // IDs of stylist's appointments
}
