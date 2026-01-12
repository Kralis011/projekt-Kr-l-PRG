using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kral_InvApp.Entities
{
        [Table("users")]
        public partial class User
        {
            [Key]
            [Column("user_id")]
            public int user_id { get; set; }
            [Column("email")]
            public string email { get; set; } = null!;
            [Column("password_hash")]
            public string password_hash { get; set; } = null!;
        }

    
}
