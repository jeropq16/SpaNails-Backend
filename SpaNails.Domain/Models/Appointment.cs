using System;
using SpaNails.Domain.Enums;

namespace SpaNails.Domain.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        
        public Guid ClientId { get; set; }
        public User Client { get; set; } = null!;

        public Guid ManicuristId { get; set; }
        public User Manicurist { get; set; } = null!;

        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public DateTime ScheduledAt { get; set; }
        
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending; 
        
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
