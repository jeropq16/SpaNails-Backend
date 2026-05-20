using System.Threading.Tasks;
using SpaNails.Domain.Models;

namespace SpaNails.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        Task<RefreshToken> GenerateRefreshTokenAsync(User user);
    }
}
