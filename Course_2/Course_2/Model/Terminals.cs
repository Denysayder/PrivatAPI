using System;
using Newtonsoft.Json;
namespace Course_2.Model
{
    public class Terminals
    {
        public List<devices> devices { get; set; }

    }
    public class devices
    {
        public string fullAddressUa { get; set; } 
        public string placeUa { get; set; }
    }
}