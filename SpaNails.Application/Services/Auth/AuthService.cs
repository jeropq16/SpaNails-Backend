using System;
using System.Threading.Tasks;
using SpaNails.Application.DTOs.Auth;
using SpaNails.Application.DTOs.Users;
using SpaNails.Application.Exceptions;
using SpaNails.Application.Interfaces;
using SpaNails.Domain.Interfaces;

namespace SpaNails.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new BadRequestException("Credenciales inválidas.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                }
            };
        }

        public Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException("Refresh token logic is not fully implemented yet.");
        }
    }
}
