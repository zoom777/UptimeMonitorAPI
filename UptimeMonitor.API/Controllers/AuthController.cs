using Microsoft.AspNetCore.Mvc;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;
using UptimeMonitor.API.Services;

namespace UptimeMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (await _userService.UserExistsAsync(dto.Username))
                return BadRequest(new { message = "Username already exists" });

            var user = await _userService.RegisterAsync(dto);
            return Ok(new { message = "User registered successfully", userId = user.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user = await _userService.ValidateUserAsync(dto);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
