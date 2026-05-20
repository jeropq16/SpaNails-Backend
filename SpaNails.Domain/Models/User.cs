using System;
using SpaNails.Domain.Enums;

namespace SpaNails.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        public UserRole Role { get; set; } = UserRole.Client; 
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
