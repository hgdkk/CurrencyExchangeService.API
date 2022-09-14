using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchangeService.Data.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string TransactionAccount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransactionAmount { get; set; }
        public string TransactionFrom { get; set; }
        public string TransactionTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransactionResult { get; set; }
        public DateTime TransactionDate { get; set; }
    }

}
