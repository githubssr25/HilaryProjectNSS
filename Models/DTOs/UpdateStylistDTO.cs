namespace Models.DTOs
{
    public class UpdateStylistDTO
    {
        public string Name { get; set; } // Allow updating the stylist's name

        public bool IsActive { get; set; } // Allow updating their active/employed status
    }
}
