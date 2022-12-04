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

namespace KreditPlus_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserTypesController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public UserTypesController(DatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUserType>>> GetTblUserTypes()
        {
            _baseJsonResponse.data = await _context.TblUserTypes.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblUserType>> GetTblUserType(int id)
        {
            var tblUserType = await _context.TblUserTypes.FindAsync(id);

            if (tblUserType == null)
            {
                return NotFound();
            }

            return tblUserType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUserType(int id, TblUserType tblUserType)
        {
            if (id != tblUserType.UserTypeId)
            {
                return BadRequest();
            }

            _context.Entry(tblUserType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TblUserType>> PostTblUserType(TblUserType tblUserType)
        {
            _context.TblUserTypes.Add(tblUserType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUserType", new { id = tblUserType.UserTypeId }, tblUserType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserType(int id)
        {
            var tblUserType = await _context.TblUserTypes.FindAsync(id);
            if (tblUserType == null)
            {
                return NotFound();
            }

            _context.TblUserTypes.Remove(tblUserType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserTypeExists(int id)
        {
            return _context.TblUserTypes.Any(e => e.UserTypeId == id);
        }
    }
}
