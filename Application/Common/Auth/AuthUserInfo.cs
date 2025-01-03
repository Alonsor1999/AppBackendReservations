namespace Application.Common.Auth
{
    public class AuthUserInfo
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Rols { get; set; }
        public int? SubCampaignId { get; set; }
    }
}
