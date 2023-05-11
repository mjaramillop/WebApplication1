using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class SandwichesExtrasController : Controller
    {
        private readonly GoodHamburguerDbContext _context;

        public SandwichesExtrasController(GoodHamburguerDbContext goodHamburguerDbContext)
        {
            _context = goodHamburguerDbContext;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllSandwichesExtras()
        {
            var extras = await _context.Extras.ToListAsync();
            var sandwiches = await _context.Sandwiches.ToListAsync();

            SandwichesExtras sandwichesExtras = new SandwichesExtras();


            foreach (var item in extras)
            {
                sandwichesExtras.Extras.Add(item);

            }
            foreach (var item in sandwiches)
            {
                sandwichesExtras.Sandwiches.Add(item);

            }



         

            return Ok(sandwichesExtras);
        }


    }
}
