using AgendaApi.Dtos;
using AgendaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaApi.Controllers
{
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;
        public AutenticacaoController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpPost("api/login")]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (login.Username != "admin" && login.Password != "admin") return Unauthorized("Usuário ou senha inválidos");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("chavemestreapiagendaconsultaprecisadebyte");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = "AgendaApi",
                Audience = "AgendaApi-client",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
