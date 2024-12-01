namespace Models.DTOs;
    public class UpdateAppointmentDTO
    {
        public int AppointmentId { get; set; }
        public int StylistId { get; set; } // Include stylist ID for potential updates
        public DateTime? TimeOf { get; set; } // Allow updating the appointment time
        public bool? IsCancelled { get; set; } // Allow toggling the cancellation status
        public List<int>? UpdatedServiceIds { get; set; } // Updated list of service IDs
    }
