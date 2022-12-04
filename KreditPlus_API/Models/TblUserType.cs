using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_API.Models
{
    public partial class TblUserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeId { get; set; }
        public string? UserTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
