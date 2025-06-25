using Microsoft.AspNetCore.Http;

namespace SimpleDotNetWebApiApp.Application.Dtos.Item
{
    public class CreateItemDto
    {
        public string? Name { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public IFormFile? Image { get; set; }

        public int CategoryId { get; set; }
    }
}
