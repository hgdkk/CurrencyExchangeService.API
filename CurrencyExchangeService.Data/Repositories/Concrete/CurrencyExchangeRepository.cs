using CurrencyExchangeService.Data.EF;
using CurrencyExchangeService.Data.Entities;
using CurrencyExchangeService.Data.Repositories.Abstract;
using Newtonsoft.Json;
using System;

namespace CurrencyExchangeService.Data.Repositories.Concrete
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly CurrencyExchangeDbContext _dbContext;
        private readonly ILogRepository _logger;

        public CurrencyExchangeRepository(CurrencyExchangeDbContext dbContext, ILogRepository logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public bool InsertExchangeTradeData(Transaction transaction)
        {
            try
            {
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
                _logger.LogInformation("InsertExchangeTradeData",$"Success, Data : {JsonConvert.SerializeObject(transaction)}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("InsertExchangeTradeData",$"Error : {ex.Message}, Data : {JsonConvert.SerializeObject(transaction)}");
                return false;
            }
        }

    }
}
