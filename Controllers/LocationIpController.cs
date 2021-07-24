using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LocationIp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationIpController : ControllerBase
    {
        private readonly postgresContext _context;

        public LocationIpController(postgresContext context)
        {
            _context = context;
        }

        // GET api/LocationIp/146.120.179.129
        [HttpGet("{Id}")]
        public async Task<ActionResult<string>> Get(ValueTuple<IPAddress, int> ip)
        { 
            var  result = _context.CityBlocks.Join(
                _context.CityLocations,
                citybl => citybl.GeonameId,
                citylk => citylk.GeonameId,
                (citybl, citylk) => new
                {
                    citybl.Network,
                    citylk.CityName,
                    citylk.CountryName,
                    citylk.CountryIsoCode,
                    citylk.TimeZone
                }
            ).Where(citybl => Equals(citybl.Network, ip));
             
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

    }
}
