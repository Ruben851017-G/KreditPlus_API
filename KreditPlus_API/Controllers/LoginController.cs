using KreditPlus_API.Entity;
using KreditPlus_API.Helper;
using KreditPlus_API.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KreditPlus_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly DatabaseContext db;
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public LoginController(DatabaseContext context, IConfiguration config, ILogger<LoginController> logger)
        {
            _configuration = config;
            db = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] TblUser tblUser)
        {
            var user =  db.TblUsers.SingleOrDefault(x => x.UserName == tblUser.UserName && x.Password == Md5Hash.getMd5Hash(tblUser.Password));
            if (user != null)
            {
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserType", user.UserType.ToString())};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:Expired"])),
                    signingCredentials: signIn);
                var _token = new JwtSecurityTokenHandler().WriteToken(token);

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        user.Token = _token;
                        user.TokenExpired = token.ValidTo;
                        user.LastLogin = DateTime.Now;
                        db.TblUsers.Update(user);
                        await db.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                _baseJsonResponse.code = Codes.Success;
                _baseJsonResponse.data = user;
            }
            else
            {
                _baseJsonResponse.code = Codes.RecordNotFound;
            }
            return Json(_baseJsonResponse);
        }
    }
}
