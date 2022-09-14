using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeService.Data.Entities
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public string Method { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
    }
}
