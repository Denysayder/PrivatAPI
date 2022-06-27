using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Course_2.Client;
using Course_2.Model;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {

        [HttpGet(Name = "ExchangeRate")]
        public List<ExchangeRate> Rates()
        {
            BankClient bankClient = new BankClient();
            return bankClient.GetExchangeRateAsync().Result;
        }
    }
}

