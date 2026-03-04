using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportApi.Data;
using TransportApi.DTOs;
using TransportApi.Helpers;

namespace TransportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtHelper _jwtHelper;

        public AuthController(AppDbContext db, JwtHelper jwtHelper)
        {
            _db = db;
            _jwtHelper = jwtHelper;
        }
        /// <summary>一次性工具：產生 BCrypt 雜湊（上線前移除）</summary>
        [HttpGet("hash")]
        [AllowAnonymous]
        public IActionResult GetHash([FromQuery] string pwd)
        {
            return Ok(BCrypt.Net.BCrypt.HashPassword(pwd));
        }
        
        /// <summary>登入，驗證帳號密碼後回傳 JWT Token</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            // 查詢帳號是否存在
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                return Unauthorized(new { message = "帳號或密碼錯誤" });

            // BCrypt 驗證密碼
            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValid)
                return Unauthorized(new { message = "帳號或密碼錯誤" });

            // 產生 JWT Token
            var token = _jwtHelper.GenerateToken(user);

            return Ok(new LoginResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Role = user.Role,
            });
        }
    }
}