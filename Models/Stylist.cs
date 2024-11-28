namespace Models;


public class Stylist
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; } // Stylist name
    public bool IsActive { get; set; } // Tracks whether stylist is still employed

    // Navigation property
    public List<Appointment> Appointments { get; set; }
    public List<StylistServiceJoinTable> StylistServiceJoinList { get; set; }
}
