using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Appointments;

namespace SpaNails.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetByIdAsync(Guid id);
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<IEnumerable<AppointmentDto>> GetByClientIdAsync(Guid clientId);
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);
        Task UpdateStatusAsync(Guid id, string status);
        Task DeleteAsync(Guid id);
    }
}
