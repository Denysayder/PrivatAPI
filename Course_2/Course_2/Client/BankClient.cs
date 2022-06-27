using System;
using Newtonsoft.Json;
using Course_2.Model;
using Course_2.Controllers;

namespace Course_2.Client
{
    public class BankClient
    {
        private HttpClient _client;
        private static string _address;
        public BankClient()
        {
            _address = Constants.address;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }
        public async Task<List<Department>> GetDepartmentsAdressesAsync(string city, string address)
        {
            var response = await _client.GetAsync($"/p24api/pboffice?json&city={city}&address={address}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Department>>(content);
            return result;
        }

        public async Task<List<ExchangeRate>> GetExchangeRateAsync()
        {
            var response = await _client.GetAsync($"/p24api/pubinfo?json&exchange&coursid=5");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<ExchangeRate>>(content);
            return result;
        }

        public async Task<Terminals> GetTerminalAdressAsync(string city)
        {
            var response = await _client.GetAsync($"/p24api/infrastructure?json&tso&address=&city={city}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Terminals>(content);
            return result;
        }

        public async Task<CurrencyArchive> GetCurrencyArchiveAsync(string date)
        {
            var response = await _client.GetAsync($"/p24api/exchange_rates?json&date={date}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<CurrencyArchive>(content);
            return result;
        }
    }
}

