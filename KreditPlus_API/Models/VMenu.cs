using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class VMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public decimal? Price { get; set; }
        public string? MenuTypeDesc { get; set; }
        public string? StatusMenuDesc { get; set; }
    }
}
