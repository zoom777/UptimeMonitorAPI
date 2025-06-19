namespace UptimeMonitor.API.DTOs
{
    public class RegisterUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
    }

    public class LoginUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}