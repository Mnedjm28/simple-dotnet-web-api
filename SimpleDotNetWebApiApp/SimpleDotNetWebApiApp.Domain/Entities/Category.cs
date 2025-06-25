using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }

        public string? Note { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}
