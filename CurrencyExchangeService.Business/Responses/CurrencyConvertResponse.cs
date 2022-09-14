using System.Collections.Generic;

namespace CurrencyExchangeService.Business.Responses
{
    public class CurrencyConvertResponse
    {
        public string Date { get; set; }
        public string Historical { get; set; }
        public Dictionary<string, object> Info { get; set; }
        public Dictionary<string, object> Query { get; set; }
        public decimal Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
