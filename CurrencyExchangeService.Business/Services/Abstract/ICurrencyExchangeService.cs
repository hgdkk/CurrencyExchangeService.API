using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;

namespace CurrencyExchangeService.Business.Services.Abstract
{
    public interface ICurrencyExchangeService
    {
        CurrencyRateResponse LatestCurrencyRates(CurrencyRateRequest request);
        CurrencyConvertResponse ConvertCurrency(CurrencyConvertRequest request);
        bool InsertExchangeTradeData(CurrencyConvertRequest request, decimal convertResult);
    }
}
