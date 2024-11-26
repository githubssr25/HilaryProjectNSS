namespace Models.DTOs;

public class CreateStylistDTO
{
    public string Name { get; set; }
    public List<int> ServiceIds { get; set; } // IDs of services the stylist specializes in
}
