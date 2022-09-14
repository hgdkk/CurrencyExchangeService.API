using CurrencyExchangeService.Data.Entities;
using System;
using System.Collections.Generic;

namespace CurrencyExchangeService.Data.Repositories.Abstract
{
    public interface ILogRepository
    {
        void LogInformation(string methodName, string message);

        void LogError(string methodName, string message);

        List<Log> GetLogList(bool? isSuccess, DateTime? logStartDate, DateTime? logEndDate);
    }
}
