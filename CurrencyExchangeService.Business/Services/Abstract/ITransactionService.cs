using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using System.Collections.Generic;

namespace CurrencyExchangeService.Business.Services.Abstract
{
    public interface ITransactionService
    {
        List<TransactionInfoResponse> GetTransactionList(TransactionInfoRequest request);
    }
}
