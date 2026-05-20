using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaNails.Domain.Interfaces;
using SpaNails.Domain.Models;

namespace SpaNails.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Manicurist)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Manicurist)
                .Include(a => a.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Manicurist)
                .Where(a => a.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByManicuristIdAsync(Guid manicuristId)
        {
            return await _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Client)
                .Where(a => a.ManicuristId == manicuristId)
                .ToListAsync();
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
