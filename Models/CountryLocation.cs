using System;
using System.Collections.Generic;

#nullable disable

namespace LocationIp
{
    public partial class CountryLocation
    {
        public long? GeonameId { get; set; }
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryIsoCode { get; set; }
        public string CountryName { get; set; }
        public bool? IsInEuropeanUnion { get; set; }
        public long CountryLocationsId { get; set; }
    }
}
