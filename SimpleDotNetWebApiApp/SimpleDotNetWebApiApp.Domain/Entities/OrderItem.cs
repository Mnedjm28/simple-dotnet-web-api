using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDotNetWebApiApp.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
