using System;
using System.Collections.Generic;
using System.Net;

#nullable disable

namespace LocationIp
{
    public partial class CountryBlock
    {
        public IPAddress? Network { get; set; }
        public long? GeonameId { get; set; }
        public long? RegisteredCountryGeonameId { get; set; }
        public long? RepresentedCountryGeonameId { get; set; }
        public bool? IsAnonymousProxy { get; set; }
        public bool? IsSatelliteProvider { get; set; }
        public long CountryBlocksId { get; set; }
    }
}
