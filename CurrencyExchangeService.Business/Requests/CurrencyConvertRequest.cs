using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeService.Business.Requests
{
    public class CurrencyConvertRequest
    {

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }
    }

}
