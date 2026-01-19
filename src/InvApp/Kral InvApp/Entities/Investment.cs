using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kral_InvApp.Entities
{
    [Table("investments")]
    public class Investment
    {
        [Key]
        [Column("investment_id")]
        public int InvestmentId { get; set; }

        [Column("asset_name")]
        public string AssetName { get; set; } = null!;

        [Column("asset_type")]
        public string AssetType { get; set; } = null!;

        [Column("buy_price")]
        public decimal BuyPrice { get; set; }

        [Column("sell_price")]
        public decimal? SellPrice { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("trade_date")]
        public DateTime TradeDate { get; set; }

        [Column("portfolio_id")]
        public int PortfolioId { get; set; }
    }
}
