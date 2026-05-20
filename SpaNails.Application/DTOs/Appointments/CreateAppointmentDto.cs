using System;

namespace SpaNails.Application.DTOs.Appointments
{
    public class CreateAppointmentDto
    {
        public Guid ClientId { get; set; }
        public Guid ManicuristId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
    }
}
