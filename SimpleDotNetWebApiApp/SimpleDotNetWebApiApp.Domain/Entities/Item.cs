using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDotNetWebApiApp.Domain.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public byte[]? Image { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}