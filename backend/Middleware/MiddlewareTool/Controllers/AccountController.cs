using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiddlewareTool.Logs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MiddlewareTool.Dto.UserMgmtDto;

namespace MiddlewareTool.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IConfiguration config) : base(config)
        { }

        [HttpPost(Name = "login")]
        public IActionResult Login(LoginModel model, string returnUrl = "")
        {
            Logging.LogInfo("Login");
            var modeLogin = _config["AuthSettings:AuthenticationMode"] ?? "";
            modeLogin = modeLogin.ToUpper();
            switch (modeLogin)
            {
                case "SQL":
                default:
                    return LoginSQL(model);
            }
            return BadRequest();
        }

        #region Private method
        private IActionResult LoginSQL(LoginModel model)
        {
            UserInfoDto user = new UserInfoDto()
            {
                UserId = "testuser",
                FullName = "Test User",
                Id = Guid.Empty
            };
            var tokenString = GenerateJwtToken(user);
            var rs = new LoginResultDto()
            {
                Token = tokenString
            };
            return Ok(rs);
        }

        private string GenerateJwtToken(UserInfoDto user)
        {
            var secret = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim("FullName", user.FullName ?? ""),
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
