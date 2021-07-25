using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LocationIp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LocationIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationIpController : ControllerBase
    {
        private readonly postgresContext _context;

        public LocationIpController(postgresContext context)
        {
            _context = context;
        }

        // GET api/CityBlock/5
        //[HttpGet("{Id}")]
        //public async Task<ActionResult<CityBlock>> Get(int id)
        //{
        //    CityBlock user = await _context.CityBlocks.FirstOrDefaultAsync(x => x.CityBlocksId == id);
        //    if (user == null)
        //        return NotFound();
        //    return new ObjectResult(user);
        //}

        //WeatherForecast/146.120.179.129
        [HttpGet]
        public IEnumerable<LocationData> Get(string ipStr)
        {
            ipStr = "146.120.179.129";

            NpgsqlConnection connection = new NpgsqlConnection(_context.Database.GetConnectionString());
            string query = $@"select city_blocks_id from city_blocks
                              inner join city_locations ON city_blocks.geoname_id = city_locations.geoname_id 
                              where network >>= '{ipStr}' limit 1;";
            connection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, connection);

            Object resultQuery = npgSqlCommand.ExecuteScalar();
            if (resultQuery != null)
            {
                LocationData locationData = new LocationData();
                var result2 = (
                    from cityb in _context.CityBlocks
                    join cityl in _context.CityLocations on cityb.GeonameId equals cityl.GeonameId
                    where cityb.CityBlocksId == (long)resultQuery
                    select new LocationData
                    {
                        CityBlockId = cityb.CityBlocksId,
                        CityName = cityl.CityName,
                        CountryName = cityl.CountryName,
                        CountryIsoCode = cityl.CountryIsoCode,
                        TimeZone = cityl.TimeZone,
                        Subdivision1Name = cityl.Subdivision1Name
                    }).AsEnumerable();

                return result2;
            }

            return null;
        }

    }
}
