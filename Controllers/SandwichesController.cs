using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class SandwichesController : Controller
    {

        private readonly GoodHamburguerDbContext _context;

        public SandwichesController(GoodHamburguerDbContext context)
        {

            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSandwiches()
        {
          var sandwiches= await  _context.Sandwiches.ToListAsync();

            return Ok(sandwiches);
        }




    }
}
