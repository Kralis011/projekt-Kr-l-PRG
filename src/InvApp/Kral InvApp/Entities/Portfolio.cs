using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kral_InvApp.Entities
{
    [Table("portfolios")]
    public class Portfolio
    {
        [Key]
        [Column("portfolio_id")]
        public int PortfolioId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}
