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
    public class TransactionsController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public TransactionsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VTransaction>>> GetTblTransactions()
        {
            _baseJsonResponse.data = await _context.VTransactions.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VTransaction>> GetTblTransaction(int id)
        {
            var tblTransaction = await _context.VTransactions.Where(m => m.OrderId == id).FirstOrDefaultAsync();

            if (tblTransaction == null)
            {
                return NotFound();
            }

            return tblTransaction;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTransaction(int id, TblTransaction tblTransaction)
        {
            if (id != tblTransaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(tblTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblTransactionExists(id))
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
        public async Task<ActionResult<TblTransaction>> PostTblTransaction(TblTransaction tblTransaction)
        {
            _context.TblTransactions.Add(tblTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblTransaction", new { id = tblTransaction.TransactionId }, tblTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblTransaction(int id)
        {
            var tblTransaction = await _context.TblTransactions.FindAsync(id);
            if (tblTransaction == null)
            {
                return NotFound();
            }

            _context.TblTransactions.Remove(tblTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblTransactionExists(int id)
        {
            return _context.TblTransactions.Any(e => e.TransactionId == id);
        }
    }
}
