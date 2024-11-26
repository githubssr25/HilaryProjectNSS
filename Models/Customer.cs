namespace Models;

public class Customer
{
    public int CustomerId { get; set; } // Primary Key
    public string Name { get; set; } // Customer name

    // Navigation property
    public List<Appointment> Appointments { get; set; }
}
