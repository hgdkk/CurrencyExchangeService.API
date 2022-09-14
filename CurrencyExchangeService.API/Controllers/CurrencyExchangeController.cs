using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using CurrencyExchangeService.Business.Services.Abstract;
using CurrencyExchangeService.Data.Core;
using CurrencyExchangeService.Data.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyExchangeService.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly IRestrictionService _restrictionService;
        private readonly ILogRepository _logger;
        private readonly ITransactionService _transactionService;
        private readonly ILogService _logService;

        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService, 
            IRestrictionService restrictionService, 
            ILogRepository logger, 
            ITransactionService transactionService,
            ILogService logService)
        {
            _currencyExchangeService = currencyExchangeService;
            _restrictionService = restrictionService;
            _logger = logger;
            _transactionService = transactionService;
            _logService = logService;
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation(Summary = "Returns real-time exchange rate data updated every 60 minutes, every 10 minutes or every 60 seconds.")]
        [HttpGet("latestCurrencyRates")]
        public CurrencyRateResponse LatestCurrencyRates(CurrencyRateRequest request)
        {
            return _currencyExchangeService.LatestCurrencyRates(request);
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation(
            Summary = "Currency conversion endpoint, which can be used to convert any amount from one currency to another.",
            Description = "Example account number : 1234")]
        [HttpGet("convertCurrency")]
        public CurrencyConvertResponse ConvertCurrency(CurrencyConvertRequest request)
        {
            var response = new CurrencyConvertResponse();

            var tradableCount = StaticConfig.HourlyTradeLimit;

            var isTradable = _restrictionService.IsTradable(request.AccountNumber, tradableCount);

            if (isTradable)
            {
                response = _currencyExchangeService.ConvertCurrency(request);
                var insertResult = _currencyExchangeService.InsertExchangeTradeData(request, response.Result);
                response.Message = insertResult == true ? "Success" : "Convertion successfull, Insertion failed";
            }
            else
            {
                response.Message = $"Hourly trade limit exceed for this account number : {request.AccountNumber}";
                _logger.LogError("LatestCurrencyRates", $"Error : {response.Message}, Request : {JsonConvert.SerializeObject(request)}");
            }
            
            return response;
        }

        [HttpGet("getTransactionList")]
        public List<TransactionInfoResponse> GetTransactionList(TransactionInfoRequest request)
        {
            var response = _transactionService.GetTransactionList(request);
            return response;
        }

        [HttpGet("getLogList")]
        public List<LogInfoResponse> GetLogList(LogInfoRequest request)
        {
            var response = _logService.GetLogList(request);
            return response;
        }

    }
}
