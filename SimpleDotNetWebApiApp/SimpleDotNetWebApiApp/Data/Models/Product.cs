using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Sku { get; set; }
    }
}
