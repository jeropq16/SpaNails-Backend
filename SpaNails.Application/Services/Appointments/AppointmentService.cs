using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Appointments;
using SpaNails.Application.Exceptions;
using SpaNails.Application.Interfaces;
using SpaNails.Domain.Enums;
using SpaNails.Domain.Interfaces;
using SpaNails.Domain.Models;

namespace SpaNails.Application.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IUserRepository userRepository, IServiceRepository serviceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<AppointmentDto> GetByIdAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) throw new NotFoundException("Cita no encontrada.");
            return MapToDto(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(MapToDto);
        }

        public async Task<IEnumerable<AppointmentDto>> GetByClientIdAsync(Guid clientId)
        {
            var appointments = await _appointmentRepository.GetByClientIdAsync(clientId);
            return appointments.Select(MapToDto);
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            var client = await _userRepository.GetByIdAsync(dto.ClientId);
            if (client == null) throw new BadRequestException("Cliente no válido.");

            var manicurist = await _userRepository.GetByIdAsync(dto.ManicuristId);
            if (manicurist == null) throw new BadRequestException("Manicurista no válida.");

            var service = await _serviceRepository.GetByIdAsync(dto.ServiceId);
            if (service == null) throw new BadRequestException("Servicio no válido.");

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                ManicuristId = dto.ManicuristId,
                ServiceId = dto.ServiceId,
                ScheduledAt = dto.ScheduledAt,
                Notes = dto.Notes,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _appointmentRepository.AddAsync(appointment);

            appointment.Client = client;
            appointment.Manicurist = manicurist;
            appointment.Service = service;

            return MapToDto(appointment);
        }

        public async Task UpdateStatusAsync(Guid id, string status)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) throw new NotFoundException("Cita no encontrada.");

            if (!Enum.TryParse<AppointmentStatus>(status, out var statusEnum))
            {
                throw new BadRequestException("Estado de cita no válido.");
            }

            appointment.Status = statusEnum;
            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _appointmentRepository.DeleteAsync(id);
        }

        private static AppointmentDto MapToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                ClientId = appointment.ClientId,
                ClientName = appointment.Client?.FirstName + " " + appointment.Client?.LastName,
                ManicuristId = appointment.ManicuristId,
                ManicuristName = appointment.Manicurist?.FirstName + " " + appointment.Manicurist?.LastName,
                ServiceId = appointment.ServiceId,
                ServiceName = appointment.Service?.Name ?? string.Empty,
                ScheduledAt = appointment.ScheduledAt,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
            };
        }
    }
}
