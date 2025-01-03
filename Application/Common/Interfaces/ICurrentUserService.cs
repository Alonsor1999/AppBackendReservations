using Application.Common.Auth;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Guid? Id { get; }
        AuthUserInfo GetUserInfo();
    }
}
