namespace Models.DTOs;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public List<int> AppointmentIds { get; set; } // IDs of customer's appointments
}
