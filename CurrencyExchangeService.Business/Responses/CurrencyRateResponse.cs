using System.Collections.Generic;

namespace CurrencyExchangeService.Business.Responses
{
    public class CurrencyRateResponse
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
        public bool Success { get; set; }
        public long Timestamp { get; set; }

        public string Message { get; set; }
    }
}
