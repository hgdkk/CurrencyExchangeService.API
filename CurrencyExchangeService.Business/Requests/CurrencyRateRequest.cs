namespace CurrencyExchangeService.Business.Requests
{
    public class CurrencyRateRequest
    {
        public string Base { get; set; }

        public string Symbols { get; set; }
    }
}
