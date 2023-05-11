using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        // very idsandwiche must be separated by com (,)
        // very idextra must be separated by com (,)

        [Key]
        public int Id { get; set; }
        [Required]
        public string IdSandwiche { get; set; }
        public string IdExtra { get; set; }


    }
}
