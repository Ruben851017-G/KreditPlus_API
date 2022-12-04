using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KreditPlus_API.Entity;
using KreditPlus_API.Models;
using Microsoft.AspNetCore.Authorization;
using KreditPlus_API.Helper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KreditPlus_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        public UsersController(DatabaseContext context, IConfiguration config, ILogger<UsersController> logger)
        {
            _configuration = config;
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VUser>>> GetTblUsers()
        {
            _baseJsonResponse.data = await _context.VUsers.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VUser>> GetTblUser(int id)
        {
            var tblUser = await _context.VUsers.Where(m => m.UserId == id).FirstOrDefaultAsync();

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUser(int id, TblUser tblUser)
        {
            if (id != tblUser.UserId)
            {
                return BadRequest();
            }

            var user  = _context.TblUsers.FirstOrDefault(m => m.UserId == id);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    user.UserFullName = tblUser.UserFullName;
                    user.UserType = tblUser.UserType;
                    if (tblUser.Password != user.Password) { user.Password = Md5Hash.getMd5Hash(tblUser.Password); }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    _baseJsonResponse.data = user;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    _baseJsonResponse.code = Codes.Error;
                }
            }
            return Json(_baseJsonResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
            tblUser.Password = Md5Hash.getMd5Hash(tblUser.Password);
            tblUser.CreatedDate = DateTime.Now;
            tblUser.Token = CreateToken();
            _context.TblUsers.Add(tblUser);
            await _context.SaveChangesAsync();
            _baseJsonResponse.code = Codes.Success;
            _baseJsonResponse.data = tblUser;
            return Json(_baseJsonResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUser(int id)
        {
            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #region GetData
        private bool TblUserExists(int id)
        {
            return _context.TblUsers.Any(e => e.UserId == id);
        }

        private string CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:Expired"])),
                signingCredentials: signIn);
            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            return _token;
        }
        #endregion
    }
}
