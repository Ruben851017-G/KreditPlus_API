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
    public class MenuTypesController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public MenuTypesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMenuType>>> GetTblMenuTypes()
        {
            _baseJsonResponse.data = await _context.TblMenuTypes.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMenuType>> GetTblMenuType(short id)
        {
            _baseJsonResponse.data = await _context.TblMenuTypes.Where(m => m.MenuTypeId == id).FirstOrDefaultAsync();

            if (_baseJsonResponse.data == null)
            {
                return NotFound();
            }

            return Json(_baseJsonResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMenuType(short id, TblMenuType tblMenuType)
        {
            if (id != tblMenuType.MenuTypeId)
            {
                return BadRequest();
            }

            _context.Entry(tblMenuType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMenuTypeExists(id))
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
        public async Task<ActionResult<TblMenuType>> PostTblMenuType(TblMenuType tblMenuType)
        {
            _context.TblMenuTypes.Add(tblMenuType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblMenuType", new { id = tblMenuType.MenuTypeId }, tblMenuType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMenuType(short id)
        {
            var tblMenuType = await _context.TblMenuTypes.FindAsync(id);
            if (tblMenuType == null)
            {
                return NotFound();
            }

            _context.TblMenuTypes.Remove(tblMenuType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMenuTypeExists(short id)
        {
            return _context.TblMenuTypes.Any(e => e.MenuTypeId == id);
        }
    }
}
