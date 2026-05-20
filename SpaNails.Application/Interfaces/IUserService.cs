using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Users;

namespace SpaNails.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}
