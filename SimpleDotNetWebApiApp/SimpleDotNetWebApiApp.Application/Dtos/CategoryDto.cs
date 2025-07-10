using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Application.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Note { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
