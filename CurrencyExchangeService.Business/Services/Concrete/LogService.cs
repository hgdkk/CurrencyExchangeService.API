using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using CurrencyExchangeService.Business.Services.Abstract;
using CurrencyExchangeService.Data.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchangeService.Business.Services.Concrete
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public List<LogInfoResponse> GetLogList(LogInfoRequest request)
        {
            var response = new List<LogInfoResponse>();
            var result = _logRepository.GetLogList(request.IsSuccess, request.LogStartDate, request.LogEndDate);

            response.AddRange(result.Select(x => new LogInfoResponse()
            {
                IsSuccess = x.IsSuccess,
                LogDate = x.LogDate,
                Message = x.Message,
                Method = x.Method
            }).ToList());

            return response;
        }

        public void LogError(string methodName, string message)
        {
            _logRepository.LogError(methodName, message);
        }

        public void LogInformation(string methodName, string message)
        {
            _logRepository.LogInformation(methodName, message);
        }
    }
}
