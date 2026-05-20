using System.Threading.Tasks;
using SpaNails.Application.DTOs.Auth;

namespace SpaNails.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken);
    }
}
