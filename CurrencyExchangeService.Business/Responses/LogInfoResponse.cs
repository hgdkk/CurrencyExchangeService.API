using System;

namespace CurrencyExchangeService.Business.Responses
{
    public class LogInfoResponse
    {
        public string Method { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime LogDate { get; set; }
    }
}
