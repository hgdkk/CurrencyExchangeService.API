using System;

namespace CurrencyExchangeService.Business.Requests
{
    public class LogInfoRequest
    {
        public bool? IsSuccess { get; set; }
        public DateTime? LogStartDate { get; set; }
        public DateTime? LogEndDate { get; set; }
    }
}
