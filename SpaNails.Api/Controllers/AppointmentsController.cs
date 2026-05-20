using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaNails.Application.DTOs.Appointments;
using SpaNails.Application.Interfaces;

namespace SpaNails.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All users must be logged in to access appointments
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manicurist")]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClient(Guid clientId)
        {
            // Optional: validate that the current user ID matches clientId or user is admin
            var appointments = await _appointmentService.GetByClientIdAsync(clientId);
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var appointment = await _appointmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Manicurist")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            await _appointmentService.UpdateStatusAsync(id, status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
