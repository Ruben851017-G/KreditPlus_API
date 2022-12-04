using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class TblTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? OrderNumber { get; set; }
        public decimal? Total { get; set; }
    }
}
