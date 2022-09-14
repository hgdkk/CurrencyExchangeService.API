namespace CurrencyExchangeService.Data.Core
{
    public class StaticConfig
    {
        public static BusinessConfig BusinessConfig = new BusinessConfig();

        public static int HourlyTradeLimit = 10;
    }

    public class BusinessConfig
    {
        public string BaseURL { get; set; }
        public string ApiKey { get; set; }
    }
}
