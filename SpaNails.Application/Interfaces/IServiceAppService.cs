using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Services;

namespace SpaNails.Application.Interfaces
{
    public interface IServiceAppService
    {
        Task<ServiceDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);
        Task DeleteAsync(Guid id);
    }
}
