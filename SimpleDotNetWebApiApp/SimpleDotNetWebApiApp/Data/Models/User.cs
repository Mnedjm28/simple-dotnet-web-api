using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDotNetWebApiApp.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<UserPermission> UserPermissions { get; set; }
    }
}
