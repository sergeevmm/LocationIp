using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocationIp;

namespace LocationIp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityBlocksController : ControllerBase
    {
        private readonly postgresContext _context;

        public CityBlocksController(postgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityBlock>>> Get()
        {
            return await _context.CityBlocks.ToListAsync();
        }

        // GET api/CityBlock/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<CityBlock>> Get(int id)
        {
            CityBlock user = await _context.CityBlocks.FirstOrDefaultAsync(x => x.CityBlocksId == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

    }
}
