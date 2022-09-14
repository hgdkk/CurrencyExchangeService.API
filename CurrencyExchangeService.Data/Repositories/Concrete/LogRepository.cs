using CurrencyExchangeService.Data.EF;
using CurrencyExchangeService.Data.Entities;
using CurrencyExchangeService.Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchangeService.Data.Repositories.Concrete
{
    public class LogRepository : ILogRepository
    {
        private readonly CurrencyExchangeDbContext _dbContext;
        public LogRepository(CurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogInformation(string methodName, string message)
        {
            Log log = new Log()
            {
                Method = methodName,
                Message = message,
                IsSuccess = true,
                LogDate = DateTime.Now
            };

            _dbContext.Logs.Add(log);
            _dbContext.SaveChanges();
        }

        public void LogError(string methodName, string message)
        {
            Log log = new Log()
            {
                Method = methodName,
                Message = message,
                IsSuccess = false,
                LogDate = DateTime.Now
            };
            _dbContext.Logs.Add(log);
            _dbContext.SaveChanges();
        }

        public List<Log> GetLogList(bool? isSuccess, DateTime? logStartDate, DateTime? logEndDate)
        {
            var query = _dbContext.Logs.AsQueryable();
            if (isSuccess.HasValue)
                query = query.Where(x => x.IsSuccess == isSuccess);
            if (logStartDate.HasValue && logEndDate.HasValue)
                query = query.Where(x => x.LogDate.Date >= ((DateTime)logStartDate).Date && x.LogDate.Date <= ((DateTime)logEndDate).Date);
            else
            {
                if (logStartDate.HasValue)
                    query = query.Where(x => x.LogDate.Date >= ((DateTime)logStartDate).Date);
                if (logEndDate.HasValue)
                    query = query.Where(x => x.LogDate.Date <= ((DateTime)logEndDate).Date);
            }

            return query.ToList();
        }
    }
}
