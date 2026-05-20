using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Services;
using SpaNails.Application.Exceptions;
using SpaNails.Application.Interfaces;
using SpaNails.Domain.Interfaces;
using SpaNails.Domain.Models;

namespace SpaNails.Application.Services.Services
{
    public class ServiceAppService : IServiceAppService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceAppService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> GetByIdAsync(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) throw new NotFoundException("Servicio no encontrado.");
            return MapToDto(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.Select(MapToDto);
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var service = new Service
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DurationInMinutes = dto.DurationInMinutes,
                IsActive = true
            };

            await _serviceRepository.AddAsync(service);
            return MapToDto(service);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _serviceRepository.DeleteAsync(id);
        }

        private static ServiceDto MapToDto(Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes,
                IsActive = service.IsActive
            };
        }
    }
}
