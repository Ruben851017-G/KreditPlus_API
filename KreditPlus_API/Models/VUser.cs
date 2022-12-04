using System;
using System.Collections.Generic;

namespace KreditPlus_API.Models
{
    public partial class VUser
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
        public string? Password { get; set; }
        public short? UserType { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpired { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? UserTypeId { get; set; }
        public string? UserTypeName { get; set; }
    }
}
