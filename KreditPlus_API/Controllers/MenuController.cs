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
    public class MenuController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();

        public MenuController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMenu>>> GetTblMenus()
        {
            _baseJsonResponse.data = await _context.VMenus.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VMenu>> GetTblMenu(int id)
        {
            var tblMenu = await _context.VMenus.Where(m => m.MenuId == id).FirstOrDefaultAsync();

            if (tblMenu == null)
            {
                return NotFound();
            }

            return tblMenu;
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMenu(int id, TblMenu tblMenu)
        {
            if (id != tblMenu.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(tblMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _baseJsonResponse.data = _context.TblMenus.Where(m => m.MenuId == id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Json(_baseJsonResponse);
        }

        [HttpPost]
        public async Task<ActionResult<TblMenu>> PostTblMenu(TblMenu tblMenu)
        {
            _context.TblMenus.Add(tblMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblMenu", new { id = tblMenu.MenuId }, tblMenu);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMenu(int id)
        {
            var tblMenu = await _context.TblMenus.FindAsync(id);
            if (tblMenu == null)
            {
                return NotFound();
            }

            _context.TblMenus.Remove(tblMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMenuExists(int id)
        {
            return _context.TblMenus.Any(e => e.MenuId == id);
        }
    }
}
