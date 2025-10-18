using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace SimpleDotNetWebApiApp.Application.Dtos
{
    public class ItemDto
    {
        [SwaggerIgnore]
        public int Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public IFormFile? Image { get; set; }

        public int CategoryId { get; set; }
    }
}
