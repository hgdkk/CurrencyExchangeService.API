namespace CurrencyExchangeService.Business.Services.Abstract
{
    public interface IRestrictionService
    {
        bool IsTradable(string accountNumber, int tradableCount);
    }
}
