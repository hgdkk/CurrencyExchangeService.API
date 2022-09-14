using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Business.Responses;
using CurrencyExchangeService.Business.Services.Abstract;
using CurrencyExchangeService.Data.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchangeService.Business.Services.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<TransactionInfoResponse> GetTransactionList(TransactionInfoRequest request)
        {
            var response = new List<TransactionInfoResponse>();
            var result = _transactionRepository.GetTransactionList(request.TransactionAccount, request.TransactionStartDate, request.TransactionEndDate);

            response.AddRange(result.Select(x => new TransactionInfoResponse()
            {
                TransactionAccount = x.TransactionAccount,
                TransactionAmount = x.TransactionAmount,
                TransactionDate = x.TransactionDate,
                TransactionFrom = x.TransactionFrom,
                TransactionResult = x.TransactionResult,
                TransactionTo = x.TransactionTo
            }).ToList());

            return response;
        }

    }
}
