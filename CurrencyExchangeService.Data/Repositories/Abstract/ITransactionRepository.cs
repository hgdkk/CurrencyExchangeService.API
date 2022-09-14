using CurrencyExchangeService.Data.Entities;
using System;
using System.Collections.Generic;

namespace CurrencyExchangeService.Data.Repositories.Abstract
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionList(string transactionAccount, DateTime? transactionStartDate, DateTime? transactionEndDate);
    }
}
