using System.ComponentModel.DataAnnotations;

namespace SimpleDotNetWebApiApp.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public string? Note { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
