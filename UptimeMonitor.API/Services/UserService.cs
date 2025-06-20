using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher;

        public UserService(AppDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> RegisterAsync(RegisterUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Role = dto.Role,
                Status = true
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> ValidateUserAsync(LoginUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username && u.Status);

            if (user == null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}