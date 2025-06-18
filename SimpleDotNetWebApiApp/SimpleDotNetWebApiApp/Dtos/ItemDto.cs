using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public IFormFile? Image { get; set; }

        public int CategoryId { get; set; }
    }
}
