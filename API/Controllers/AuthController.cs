using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly IConfiguration _config;

        public AuthController(IUserService users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> AddUser(RegisterUserRequest request)
        {
            var user = await _users.AddUser(request);

            if (user == null)
                return Conflict(new { message = "Email is already registered" });

            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                role = user.Role,
                createdAt = user.CreatedAt
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _users.GetByEmail(request.Email);
            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            var claims = new[]
 {
    new Claim(ClaimTypes.NameIdentifier, user.Id),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role)
};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        // 🔹 UPDATE USER PROFILE
        [Authorize]
        [HttpPut("updatecurrentuser")]
        public async Task<IActionResult> UpdateProfile(UpdateUserRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(email))
                return Unauthorized("Email claim missing in token");

            var updatedUser = await _users.UpdateByEmail(email, request);

            return Ok(new
            {
                id = updatedUser.Id,
                email = updatedUser.Email,
                firstName = updatedUser.FirstName,
                lastName = updatedUser.LastName,
                role = updatedUser.Role,
                createdAt = updatedUser.CreatedAt
            });
        }


     


    }

    public record LoginRequest(string Email, string Password);
}
