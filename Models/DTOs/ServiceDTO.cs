namespace Models.DTOs;

public class ServiceDTO
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
}
