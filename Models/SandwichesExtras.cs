using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [NotMapped]
    public class SandwichesExtras
    {

        public SandwichesExtras()
        {
            this.Extras= new List<Extra>();
            this.Sandwiches = new List<Sandwiche>();

        }


        [Key]
        public int Id { get; set; }

        public List<Sandwiche> Sandwiches { get; set; }

        public List<Extra> Extras { get; set; }

    }
}
