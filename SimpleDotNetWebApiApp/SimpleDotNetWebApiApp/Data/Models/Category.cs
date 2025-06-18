using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public string? Note { get; set; }

        public List<Item> Items { get; set; }
    }
}
