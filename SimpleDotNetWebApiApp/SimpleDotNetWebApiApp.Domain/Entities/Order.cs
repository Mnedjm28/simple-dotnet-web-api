using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string? Reference { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
