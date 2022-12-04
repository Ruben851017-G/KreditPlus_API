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
using System.Text.RegularExpressions;

namespace KreditPlus_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _context;
        public BaseJsonResponse _baseJsonResponse = new BaseJsonResponse();
        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VOrder>>> GetTblOrders()
        {
            _baseJsonResponse.data = await _context.VOrders.ToListAsync();
            return Json(_baseJsonResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VOrder>> GetTblOrder(int id)
        {
            var tblOrder = await _context.VOrders.Where(m => m.OrderId == id).FirstOrDefaultAsync();

            if (tblOrder == null)
            {
                return NotFound();
            }

            return tblOrder;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrder(int id, TblOrder tblOrder)
        {
            if (id != tblOrder.OrderId)
            {
                return BadRequest();
            }

            var order = _context.TblOrders.FirstOrDefault(m => m.OrderId == id);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    order.OrderStatus = tblOrder.OrderStatus;
                    order.ClosedBy = tblOrder.ClosedBy;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    _baseJsonResponse.data = order;
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
        public async Task<ActionResult<TblOrder>> PostTblOrder(TblOrder tblOrder)
        {
            tblOrder.OrderNumber = GenerateOrderNumber();
            _context.TblOrders.Add(tblOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblOrder", new { id = tblOrder.OrderId }, tblOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblOrder(int id)
        {
            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            _context.TblOrders.Remove(tblOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblOrderExists(int id)
        {
            return _context.TblOrders.Any(e => e.OrderId == id);
        }

        private string GenerateOrderNumber()
        {
            string result = null;
            string _now = DateTime.Now.ToString("dd-MM-yyyy").Replace("-","");
            int counter = 1;
            var data = _context.TblOrders.OrderByDescending(m => m.OrderId).FirstOrDefault();
            if (data != null)
            {
                if (data.OrderNumber == null)
                {
                    result = "ABC03122022-001"; // set default if first row order number when empty
                    return result;
                }
            }
            var pattern = @"^[a-zA-Z]+";
            var strPart = Regex.Match(data.OrderNumber, pattern).Value; // find existing if order number not contain standart format
            if (strPart == "" || strPart != "ABC")
            {
                result = "ABC03122022-001";
                return result;
            }
            var noPart = Regex.Replace(data.OrderNumber, pattern, "");
            string soutput = noPart.Substring(noPart.IndexOf('-') + 1);
            var no = int.Parse(soutput);
            var length = soutput.Length;
            length = (no + 1) / (Math.Pow(10, length)) == 1 ? length + counter : length;
            var output = strPart + _now +(no + 1).ToString("D" + length);
            result = output;
            return result;
        }
    }
}
