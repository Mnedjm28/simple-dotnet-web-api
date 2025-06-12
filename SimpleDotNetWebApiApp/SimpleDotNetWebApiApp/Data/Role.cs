using Microsoft.Extensions.Hosting;

namespace SimpleDotNetWebApiApp.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
    }
}
