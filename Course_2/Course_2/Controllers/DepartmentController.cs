using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course_2.Client;
using Course_2.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet(Name = "Department")]
        public List<Department> departments(string city, string address)
        {
            BankClient bankClient = new BankClient();
            return bankClient.GetDepartmentsAdressesAsync(city, address).Result;
        }
    }
}

