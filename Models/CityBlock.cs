using System;
using System.Collections.Generic;
using System.Net;

#nullable disable

namespace LocationIp
{
    public class CityBlock
    {
        public IPAddress? Network { get; set; }
        public long? GeonameId { get; set; }
        public long? RegisteredCountryGeonameId { get; set; }
        public long? RepresentedCountryGeonameId { get; set; }
        public bool? IsAnonymousProxy { get; set; }
        public bool? IsSatelliteProvider { get; set; }
        public string PostalCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public short? AccuracyRadius { get; set; }
        public long CityBlocksId { get; set; }
    }
}
