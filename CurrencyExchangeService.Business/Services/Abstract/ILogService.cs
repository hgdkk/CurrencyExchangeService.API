using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using System.Collections.Generic;

namespace CurrencyExchangeService.Business.Services.Abstract
{
    public interface ILogService
    {
        void LogInformation(string methodName, string message);
        void LogError(string methodName, string message);
        List<LogInfoResponse> GetLogList(LogInfoRequest request);
    }
}
