using System;
using System.Collections.Generic;

namespace KreditPlus_API.Models
{
    public partial class VTransaction
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? UserWaitersOrder { get; set; }
        public string UserWaitersNameOrder { get; set; }
        public decimal? Total { get; set; }
    }
}
