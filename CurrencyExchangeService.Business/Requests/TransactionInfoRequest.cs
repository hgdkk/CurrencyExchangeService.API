using System;

namespace CurrencyExchangeService.Business.Requests
{
    public class TransactionInfoRequest
    {
        public string TransactionAccount { get; set; }
        public DateTime? TransactionStartDate { get; set; }
        public DateTime? TransactionEndDate { get; set; }
    }
}
