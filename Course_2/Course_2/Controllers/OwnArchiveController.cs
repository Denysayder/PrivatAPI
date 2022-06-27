using System;
using Course_2.Client;
using Course_2.Model;
using Microsoft.AspNetCore.Mvc;

namespace Course_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnArchiveController : ControllerBase
    {
        private readonly ILogger<ExchangeRateController> _logger;
        private readonly IDynamodbClient _dynamoDbClient;

        public OwnArchiveController(ILogger<ExchangeRateController> logger, IDynamodbClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDbClient;
        }

        [HttpPost(Name = "AddNewCurrency")]
        public async Task<IActionResult> AddCurrency([FromBody] citydbrepository citydbrepository)
        {
            await _dynamoDbClient.PostDepartmentInfo(citydbrepository);
            return Ok();
        }

        [HttpGet(Name = "GetAllArchive")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _dynamoDbClient.GetAll();
            if (response == null)
                return NotFound("There are no records in DB");
            var result = response
                .Select(x => new citydbrepository()
                {
                    Time = x.Time,
                    ccy = x.ccy,
                    base_ccy = x.base_ccy,
                    buy = x.buy,
                    sale = x.sale
                })
                .ToList();
            return Ok(result);

        }

        [HttpDelete(Name = "DeleteCurrency")]
        public async Task<IActionResult> DeleteCurrency()
        {
            var response = await _dynamoDbClient.GetAll();
            if (response == null)
                return NotFound("There are no records in DB");
            var result = response
                .Select(x => new citydbrepository()
                {
                    ID = x.ID,
                    Time = x.Time,
                })
                .ToList();
            foreach (var n in result)
                await _dynamoDbClient.DeleteArchiveInfo(n.ID, n.Time);
            return Ok();
        }

        [HttpPut(Name = "UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency(string ID, string Time, string NewBuy, string NewSale)
        {
            await _dynamoDbClient.UpdateCurrencyAsync(ID, Time, NewBuy, NewSale);
            return Ok();
        }
    }
}

