using CurrencyExchangeService.Data.Entities;

namespace CurrencyExchangeService.Data.Repositories.Abstract
{
    public interface ICurrencyExchangeRepository
    {
        public bool InsertExchangeTradeData(Transaction transaction);
    }
}
