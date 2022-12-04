using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class TblMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public short? StatusMenuId { get; set; }
        public short? MenuTypeId { get; set; }
        public decimal? Price { get; set; }
    }
}
