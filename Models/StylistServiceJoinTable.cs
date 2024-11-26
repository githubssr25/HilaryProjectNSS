namespace Models;

public class StylistServiceJoinTable
{
    public int StylistServiceId { get; set; } // Single-column primary key
    public int StylistId { get; set; } // Foreign Key Part 1
    public int ServiceId { get; set; } // Foreign Key Part 2

    // Navigation properties
    public Stylist Stylist { get; set; }
    public Service Service { get; set; }
}
