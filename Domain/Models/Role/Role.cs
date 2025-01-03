namespace Domain.Models.Role
{
    public class Role : Entity.Entity
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public bool Status { get; set; } = true;
    }
}
