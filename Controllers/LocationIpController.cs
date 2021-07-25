using Microsoft.AspNetCore.Mvc; 
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
         
        //LocationIp/146.120.179.129
        [Route("{ipStr}")]
        [HttpGet]
        public ActionResult<LocationData> Get(string ipStr)
        {
            IPAddress ipAddr;
            if (!IPAddress.TryParse(ipStr, out ipAddr)) 
                ModelState.AddModelError("IPAddress", "Не корректный IP-адрес"); 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            NpgsqlConnection connection = new NpgsqlConnection(_context.Database.GetConnectionString());
            connection.Open();

            string query = $@"select city_blocks_id from city_blocks
                              inner join city_locations ON city_blocks.geoname_id = city_locations.geoname_id 
                              where network >>= '{ipStr}' limit 1;";   
            string query2 = $"select autonomous_system_organization from asn_blocks ab where network >>= '{ipStr}'";

            var name = GetValueBySqlQuery(connection, query2); 
            var resultQuery = GetValueBySqlQuery(connection, query);

            if (resultQuery != null)
            { 
                var locationDataEnumerable = (
                    from cityb in _context.CityBlocks
                    join cityl in _context.CityLocations on cityb.GeonameId equals cityl.GeonameId
                    where cityb.CityBlocksId == (long)resultQuery
                    select new LocationData
                    { 
                        ContinentName = name == null ? string.Empty : name.ToString(),
                        CityName = cityl.CityName,
                        CountryName = cityl.CountryName,
                        CountryIsoCode = cityl.CountryIsoCode,
                        TimeZone = cityl.TimeZone,
                        Subdivision1Name = cityl.Subdivision1Name
                    }).AsEnumerable();

                return new ObjectResult(locationDataEnumerable); 
            }
            ModelState.AddModelError("IPAddress", "IP-адрес не найден");
            return BadRequest(ModelState);
        }

        private object GetValueBySqlQuery(NpgsqlConnection connection, string query)
        {
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, connection); 
            return npgSqlCommand.ExecuteScalar();  
        }
         
    }
}
