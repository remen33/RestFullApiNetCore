namespace SocialMedia.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService securityService;
        public TokenController(IConfiguration configuration, ISecurityService securityService)
        {
            this.securityService = securityService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {            
            var security = await this.IsValidUser(login);

            if (security != null)
            {
                var token = GenerateToken(security);
                return Ok(new { token });
            }

            return NotFound();            
        }

        private async Task<Security> IsValidUser(UserLogin login)
        {
            return await securityService.GetLogginByCredentials(login);            
        }

        private string GenerateToken(Security security)
        {
            // Header 
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                new Claim("User", security.User),
                new Claim(ClaimTypes.Role, security.Role.ToString())
            };


            // PayLoad
            var payload = new JwtPayload(
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claims,
               DateTime.Now,
               DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
