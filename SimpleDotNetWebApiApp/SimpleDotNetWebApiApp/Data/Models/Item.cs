using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace SimpleDotNetWebApiApp.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public double Price { get; set; }

        public string? Note { get; set; }

        public byte[]? Image { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}