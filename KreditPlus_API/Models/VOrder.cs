using System;
using System.Collections.Generic;

namespace KreditPlus_API.Models
{
    public partial class VOrder
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? StatusOrder { get; set; }
        public string? Cashier { get; set; }
        public string? Waiters { get; set; }
        public short? TableNumber { get; set; }
        public decimal? Total { get; set; }
        public string? ClosedByName { get; set; }
    }
}
