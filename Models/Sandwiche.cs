using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Sandwiche
    {

        [Key]
        public int Id {get;set;}

        public string? Name { get;set;}

        [Column(TypeName = "decimal(18,4)")]
        public decimal Value { get;set;}



    }
}
