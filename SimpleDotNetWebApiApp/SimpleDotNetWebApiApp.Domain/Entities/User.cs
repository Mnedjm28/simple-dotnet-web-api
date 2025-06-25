using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDotNetWebApiApp.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
