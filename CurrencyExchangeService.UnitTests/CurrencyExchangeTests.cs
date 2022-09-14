using CurrencyExchangeService.Business.Requests;
using CurrencyExchangeService.Data.Core;
using CurrencyExchangeService.Data.EF;
using CurrencyExchangeService.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CurrencyExchangeService.UnitTests
{
    public class CurrencyExchangeTests : IDisposable
    {
        private Business.Services.Concrete.CurrencyExchangeService _currencyExchangeService;
        private Mock<ICurrencyExchangeRepository> _currencyExchangeRepository;
        private Mock<ILogRepository> _logger;
        private IConfigurationRoot _config;
        CurrencyExchangeDbContext _context;

        [SetUp]
        public void Setup()
        {
            _currencyExchangeRepository = new Mock<ICurrencyExchangeRepository>();
            _logger = new Mock<ILogRepository>();
            _config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            _config.GetSection("BusinessConfig").Bind(StaticConfig.BusinessConfig);
            _currencyExchangeService = new Business.Services.Concrete.CurrencyExchangeService(_currencyExchangeRepository.Object, _logger.Object);

            var _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<CurrencyExchangeDbContext>();
            builder.UseSqlServer(_config.GetConnectionString("CurrencyExchangeDBContext")).UseInternalServiceProvider(_serviceProvider);
            _context = new CurrencyExchangeDbContext(builder.Options);
            _context.Database.Migrate();
        }

        [Test]
        public void LatestCurrencyRates_WhenCalledWithoutParameters_ReturnTrue()
        {
            var request = new CurrencyRateRequest();
            var latestCurrencyRateList = _currencyExchangeService.LatestCurrencyRates(request);
            Assert.That(latestCurrencyRateList.Success);
        }

        [Test]
        public void LatestCurrencyRates_WhenCalledWithoutParameters_ReturnList()
        {
            var request = new CurrencyRateRequest();
            var latestCurrencyRateList = _currencyExchangeService.LatestCurrencyRates(request);
            Assert.That(latestCurrencyRateList.Rates.Count > 0);
        }

        [Test]
        public void LatestCurrencyRates_WhenCalledWithAbsurdParameters_ReturnFalse()
        {
            var request = new CurrencyRateRequest();
            request.Base = "HG";
            request.Symbols = "GH";
            var latestCurrencyRateList = _currencyExchangeService.LatestCurrencyRates(request);
            Assert.That(latestCurrencyRateList.Success == false);
        }

        [Test]
        public void ConvertCurrency_WhenCalledWithExactParameters_ReturnTrue()
        {
            var request = new CurrencyConvertRequest();
            request.AccountNumber = "1234";
            request.Amount = 100;
            request.From = "GBP";
            request.To = "TRY";

            var response = _currencyExchangeService.ConvertCurrency(request);
            Assert.That(response.Success);
        }

        [Test]
        public void ConvertCurrency_WhenCalledWithAbsurdParameters_ReturnFalse()
        {
            var request = new CurrencyConvertRequest();
            request.AccountNumber = "5678";
            request.Amount = 100;
            request.From = "HG";
            request.To = "GH";

            var response = _currencyExchangeService.ConvertCurrency(request);
            Assert.That(response.Success == false);
        }

        [Test]
        public void InsertTransaction_WhenCalled_ReturnInsertedTransactionIsNotNull()
        {
            _context.Transactions.Add(new Data.Entities.Transaction
            {
                TransactionAccount = "TestAccount",
                TransactionAmount = 999,
                TransactionDate = DateTime.Now,
                TransactionFrom = "USD",
                TransactionTo = "USD",
                TransactionResult = 999
            });
            _context.SaveChanges();

            var query = "SELECT * FROM [dbo].[Transactions] WHERE TransactionAccount = 'TestAccount'";
            var transaction = _context.Transactions.FromSqlRaw(query).First();
            Assert.That(transaction != null);
        }

        [Test]
        public void InsertLog_WhenCalled_ReturnInsertedLogIsNotNull()
        {
            _context.Logs.Add(new Data.Entities.Log
            {
                IsSuccess = true,
                LogDate = DateTime.Now,
                Message = "TestMessage",
                Method = "TestMethod"
            });
            _context.SaveChanges();

            var query = "SELECT * FROM [dbo].[Logs] WHERE Method = 'TestMethod'";
            var log = _context.Logs.FromSqlRaw(query).First();
            Assert.That(log != null);
        }

        public void Dispose()
        {
            var transaction = _context.Transactions.First(x => x.TransactionAccount == "TestAccount");
            _context.Transactions.Remove(transaction);

            var log = _context.Logs.First(x => x.Method == "TestMethod");
            _context.Logs.Remove(log);

            _context.SaveChanges();
        }
    }
}
