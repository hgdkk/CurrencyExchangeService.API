using CurrencyExchangeService.Data.EF;
using CurrencyExchangeService.Data.Entities;
using CurrencyExchangeService.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchangeService.Data.Repositories.Concrete
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CurrencyExchangeDbContext _dbContext;
        public TransactionRepository(CurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Transaction> GetTransactionList(string transactionAccount, DateTime? transactionStartDate, DateTime? transactionEndDate)
        {
            var query = _dbContext.Transactions.AsQueryable();
            if (!string.IsNullOrEmpty(transactionAccount))
                query = query.Where(x => x.TransactionAccount == transactionAccount);
            if (transactionStartDate.HasValue && transactionEndDate.HasValue)
                query = query.Where(x => x.TransactionDate.Date >= ((DateTime)transactionStartDate).Date && x.TransactionDate.Date <= ((DateTime)transactionEndDate).Date);
            else
            {
                if (transactionStartDate.HasValue)
                    query = query.Where(x => x.TransactionDate.Date >= ((DateTime)transactionStartDate).Date);
                if(transactionEndDate.HasValue)
                    query = query.Where(x => x.TransactionDate.Date <= ((DateTime)transactionEndDate).Date);
            }

            return query.ToList();
        }
    }
}
