using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class TblOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        public string? OrderId { get; set; }
        public int? MenuId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
    }
}
