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
    public class StatusMenusController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public StatusMenusController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblStatusMenu>>> GetTblStatusMenus()
        {
            _baseJsonResponse.data = await _context.TblStatusMenus.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblStatusMenu>> GetTblStatusMenu(short id)
        {
            var tblStatusMenu = await _context.TblStatusMenus.FindAsync(id);

            if (tblStatusMenu == null)
            {
                return NotFound();
            }

            return tblStatusMenu;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblStatusMenu(short id, TblStatusMenu tblStatusMenu)
        {
            if (id != tblStatusMenu.StatusMenuId)
            {
                return BadRequest();
            }

            _context.Entry(tblStatusMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblStatusMenuExists(id))
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
        public async Task<ActionResult<TblStatusMenu>> PostTblStatusMenu(TblStatusMenu tblStatusMenu)
        {
            _context.TblStatusMenus.Add(tblStatusMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblStatusMenu", new { id = tblStatusMenu.StatusMenuId }, tblStatusMenu);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblStatusMenu(short id)
        {
            var tblStatusMenu = await _context.TblStatusMenus.FindAsync(id);
            if (tblStatusMenu == null)
            {
                return NotFound();
            }

            _context.TblStatusMenus.Remove(tblStatusMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblStatusMenuExists(short id)
        {
            return _context.TblStatusMenus.Any(e => e.StatusMenuId == id);
        }
    }
}
