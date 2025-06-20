using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;

namespace UptimeMonitor.API.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}