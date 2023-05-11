using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Utils
{
    public class Validations
    {
        private readonly GoodHamburguerDbContext _context;


        public Validations(GoodHamburguerDbContext goodHamburguerDbContext) 
        {
            _context = goodHamburguerDbContext;
        }

        public string validate(Order order)
        {
            string msgerror = "";
            string[] arrayextras = order.IdExtra.Split(",");
            bool valuerepeated = false;
            foreach (var grouping in arrayextras.GroupBy(t => t).Where(t => t.Count() != 1))
            {
                msgerror = string.Format("'{0}' is repeated {1} times.", grouping.Key, grouping.Count());
                return msgerror;
            }

            string _sandwiches = order.IdSandwiche;
            if (_sandwiches.Length > 1)
            {
                msgerror = "the order can not contain more than one sanwiche";
                return msgerror;
            }




            var sandwichefound = _context.Sandwiches.FirstOrDefault(a => a.Id == Convert.ToUInt16(order.IdSandwiche));

            if (sandwichefound == null)
            {
                msgerror = "Id sandwiche not found ";
                return msgerror;

            }

            if (!order.IdExtra.Equals(null))
            {
                foreach (var element in arrayextras)
                {
                    var extrafound = _context.Extras.FirstOrDefault(a => a.Id == Convert.ToInt16(element));
                    if (extrafound == null)
                    {
                        msgerror = "Id extra not found " + extrafound;
                        return msgerror;

                    }

                }


            }

            return msgerror;

        }

    }
}
