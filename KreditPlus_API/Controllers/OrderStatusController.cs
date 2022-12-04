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

namespace KreditPlus_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderStatusController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public OrderStatusController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrderStatus>>> GetTblOrderStatuses()
        {
            _baseJsonResponse.data = await _context.TblOrderStatuses.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrderStatus>> GetTblOrderStatus(short id)
        {
            var tblOrderStatus = await _context.TblOrderStatuses.FindAsync(id);

            if (tblOrderStatus == null)
            {
                return NotFound();
            }

            return tblOrderStatus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrderStatus(short id, TblOrderStatus tblOrderStatus)
        {
            if (id != tblOrderStatus.OrderStatusId)
            {
                return BadRequest();
            }

            var status = _context.TblOrderStatuses.FirstOrDefault(m => m.OrderStatusId == id);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    status.OrderStatusDesc = tblOrderStatus.OrderStatusDesc;
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    _baseJsonResponse.data = status;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    _baseJsonResponse.code = Codes.Error;
                }
            }
            return Json(_baseJsonResponse);
        }

        [HttpPost]
        public async Task<ActionResult<TblOrderStatus>> PostTblOrderStatus(TblOrderStatus tblOrderStatus)
        {
            _context.TblOrderStatuses.Add(tblOrderStatus);
            await _context.SaveChangesAsync();
            _baseJsonResponse.data = tblOrderStatus;
            return Json(_baseJsonResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblOrderStatus(short id)
        {
            var tblOrderStatus = await _context.TblOrderStatuses.FindAsync(id);
            if (tblOrderStatus == null)
            {
                return NotFound();
            }

            _context.TblOrderStatuses.Remove(tblOrderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblOrderStatusExists(short id)
        {
            return _context.TblOrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}
