using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Xml.Linq;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        string[] arrayextras = null;  
        private readonly GoodHamburguerDbContext _context;

        public OrdersController(GoodHamburguerDbContext goodHamburguerDbContext)
        {
            _context = goodHamburguerDbContext;
        }


        [HttpGet]
        [ActionName("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            var extras = await _context.Extras.ToListAsync();
            var sandwiches = await _context.Sandwiches.ToListAsync();


            List<OrdersDTO> listorders = new List<OrdersDTO>();

            foreach (var order in orders)
            {
                var sandwichefound = sandwiches.FirstOrDefault(a => a.Id == Convert.ToUInt16(order.IdSandwiche));

                OrdersDTO ordersdto = new OrdersDTO();
                ordersdto.Sandwiche = sandwichefound.Name;
                ordersdto.Order = "Order No " + order.Id;
                string[] arrayextras = order.IdExtra.Split(",");
                foreach (var item in arrayextras)
                {
                    var extra = extras.Where(a => a.Id == Convert.ToInt16(item)).FirstOrDefault<Extra>();
                   ordersdto.Extra = ordersdto.Extra+ extra.Name + " - ";

                }

                listorders.Add(ordersdto);

              
            }
            return Ok(listorders);

        }



        // every idsandwiche must be separated by com (,)
        // every idextra must be separated by com (,)


        [HttpPost]

        public async Task<IActionResult> AddOrder([FromBody] Order order )
        {
            Validations vl = new Validations(_context);

            string msgerror = vl.validate(order);
            if (msgerror.Trim().Length>0) { return Ok(msgerror); };

            await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

            var orders = await _context.Orders.ToListAsync();
            var sandwiches = await _context.Sandwiches.ToListAsync();
            var extras = await _context.Extras.ToListAsync();
            double totalamount = 0;

            foreach (var item in orders)
            {

                double percentagediscount = 0;
                string itemsorder = item.IdSandwiche + item.IdExtra.Replace(",","");
                if (itemsorder.Length == 3) percentagediscount = 20.0;
                if (item.IdExtra == "1") percentagediscount = 10.0;
                if (item.IdExtra == "2") percentagediscount = 15.0;

                var sandwiche = sandwiches.Where(a => a.Id == Convert.ToInt16( item.IdSandwiche)).FirstOrDefault<Sandwiche>();

                double totalparcial = 0;
                totalparcial = totalparcial + Convert.ToDouble( sandwiche.Value);

                arrayextras=item.IdExtra.Split(',');
                foreach (var element in arrayextras)
                {

                    var extra = extras.Where(a => a.Id == Convert.ToInt16(element)).FirstOrDefault<Extra>();
                    totalparcial = totalparcial +Convert.ToDouble( extra.Value);

                }
                double pr = percentagediscount / 100;
                double valuediscount = totalparcial * pr;

                totalparcial= totalparcial - valuediscount;
                totalamount = totalamount + (totalparcial );

            }

            return Ok("Total Amount: "+totalamount);// CreatedAtAction(nameof(GetAllOrders),);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Order order)
        {

            Validations vl = new Validations( _context);
           
           string msgerror = vl.validate(order);

            //   if (msgerror.Trim().Length > 0) { return Ok(msgerror); }

            var existingorder =   await _context.Orders.FindAsync(order.Id);

            if (existingorder != null)
            {
                existingorder.IdSandwiche= order.IdSandwiche;
                existingorder.IdExtra= order.IdExtra;
                await _context.SaveChangesAsync();
                return Ok(existingorder);
            }
            return NotFound("Order not found");


        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Order order)
        {

           
            var existingorder = await _context.Orders.FindAsync(order.Id);

            if (existingorder != null)
            {
                 _context.Remove(existingorder);
                await _context.SaveChangesAsync();
                return Ok(existingorder);
            }
            return NotFound("Order not found");


        }



    }
}
