using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.User
{
    public class User : Entity.Entity
    {
        public string? Document { get; set; }
        public string? Names { get; set; }
        public string? Phone1 { get; set; }
        public string? Email { get; set; }
        public bool Status { get; set; } = true;
        public bool Active { get; set; } = true;
        public string? Password { get; set; }
        public string? Login { get; set; }
        public Guid? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role.Role? Role { get; set; }

    }
}
