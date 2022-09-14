using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using CurrencyExchangeService.Business.Services.Abstract;
using CurrencyExchangeService.Data.Core;
using CurrencyExchangeService.Data.Entities;
using CurrencyExchangeService.Data.Repositories.Abstract;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace CurrencyExchangeService.Business.Services.Concrete
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;
        private readonly ILogRepository _logger;

        public CurrencyExchangeService(ICurrencyExchangeRepository currencyExchangeRepository,
            ILogRepository logger
            )
        {
            _currencyExchangeRepository = currencyExchangeRepository;
            _logger = logger;
        }
        public CurrencyRateResponse LatestCurrencyRates(CurrencyRateRequest request)
        {
            var url = StaticConfig.BusinessConfig.BaseURL + "latest";

            if (!string.IsNullOrEmpty(request.Symbols) && !string.IsNullOrEmpty(request.Base))
                url += $"?symbols={request.Symbols}&base={request.Base}";
            else
            {
                if (!string.IsNullOrEmpty(request.Symbols))
                    url += $"?symbols={request.Symbols}";
                if (!string.IsNullOrEmpty(request.Base))
                    url += $"?base={request.Base}";
            }

            var response = new CurrencyRateResponse();

            var restResponse = ApiCall(url);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<CurrencyRateResponse>(restResponse.Content);
                response.Message = "Success";
                _logger.LogInformation("LatestCurrencyRates", $"Success, Request : {JsonConvert.SerializeObject(request)}");
            }
            else if (restResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                response.Message = "API request limit exceeded";
                _logger.LogError("LatestCurrencyRates", $"Error : API request limit exceeded, Request : {JsonConvert.SerializeObject(request)}");
            }
            else
            {
                response.Message = restResponse.ErrorMessage;
                _logger.LogError("LatestCurrencyRates", $"Error : {response.Message}, Request : {JsonConvert.SerializeObject(request)}");
            }

            return response;

        }

        public CurrencyConvertResponse ConvertCurrency(CurrencyConvertRequest request)
        {
            var response = new CurrencyConvertResponse();

            var url = $"{StaticConfig.BusinessConfig.BaseURL}convert?to={request.To}&from={request.From}&amount={request.Amount.ToString().Replace(",", ".")}";

            var restResponse = ApiCall(url);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<CurrencyConvertResponse>(restResponse.Content);
                response.Message = "Success";
                _logger.LogInformation("ConvertCurrency", $"{response.Message}, Request : {JsonConvert.SerializeObject(request)}");
            }
            else if (restResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                response.Message = "API request limit exceeded";
                _logger.LogError("ConvertCurrency", $"Error : API request limit exceeded, Request : {JsonConvert.SerializeObject(request)}");
            }
            else
            {
                response.Message = restResponse.ErrorMessage;
                _logger.LogError("ConvertCurrency", $"Error : {response.Message}, Request : {JsonConvert.SerializeObject(request)}");
            }


            return response;
        }

        public static RestResponse ApiCall(string url)
        {
            var client = new RestClient(url);

            var restRequest = new RestRequest();
            restRequest.Method = Method.Get;
            restRequest.AddHeader("apikey", StaticConfig.BusinessConfig.ApiKey);

            return client.Execute(restRequest);
        }

        public bool InsertExchangeTradeData(CurrencyConvertRequest request, decimal convertResult)
        {
            Transaction transaction = new Transaction()
            {
                TransactionAccount = request.AccountNumber,
                TransactionAmount = request.Amount,
                TransactionFrom = request.From,
                TransactionResult = convertResult,
                TransactionTo = request.To,
                TransactionDate = DateTime.Now
            };

            var result = _currencyExchangeRepository.InsertExchangeTradeData(transaction);
            return result;
        }
    }
}
