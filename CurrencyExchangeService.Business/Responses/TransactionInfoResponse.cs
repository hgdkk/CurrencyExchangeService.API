using System;

namespace CurrencyExchangeService.Business.Responses
{
    public class TransactionInfoResponse
    {
        public string TransactionAccount { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionFrom { get; set; }
        public string TransactionTo { get; set; }
        public decimal TransactionResult { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
