using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;

namespace UptimeMonitor.API.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterUserDto dto);
        Task<User?> ValidateUserAsync(LoginUserDto dto);
        Task<bool> UserExistsAsync(string username);
    }
}