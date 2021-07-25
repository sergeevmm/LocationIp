using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LocationIp.Models
{
    public class LocationData
    {
        public long CityBlockId { get; set; } 
        public string ContinentName { get; set; }
        public string CityName { get; set; }
        public string Subdivision1Name { get; set; }
        public string CountryName { get; set; }
        public string CountryIsoCode { get; set; }
        public string TimeZone { get; set; }
    }
}
