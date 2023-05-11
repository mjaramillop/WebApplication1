using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtrasController : Controller
    {
        private readonly GoodHamburguerDbContext _context;

        public ExtrasController(GoodHamburguerDbContext goodHamburguerDbContext)
        {
            _context = goodHamburguerDbContext;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllExtras()
        {
            var extras = await _context.Extras.ToListAsync();

            return Ok(extras);
        }

    }
}
