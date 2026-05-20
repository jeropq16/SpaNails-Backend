using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaNails.Domain.Models;

namespace SpaNails.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<Appointment>> GetByManicuristIdAsync(Guid manicuristId);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Guid id);
    }
}
