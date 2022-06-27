using System;
using Newtonsoft.Json;

namespace Course_2.Model
{
    public class CurrencyArchive
    {
        public List<exchangeRate> exchangeRate { get; set; }
    }

    public class exchangeRate
    {
        public string baseCurrency { get; set; }
        public string currency { get; set; }
        public string saleRate { get; set; }
        public string purchaseRate { get; set; }
    }
}


