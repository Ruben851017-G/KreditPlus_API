using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class TblOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public short? OrderStatus { get; set; }
        public int? CreatedByCashier { get; set; }
        public int? OrderedByWaiters { get; set; }
        public short? TableNumber { get; set; }
        public int? ClosedBy { get; set; }
    }
}
