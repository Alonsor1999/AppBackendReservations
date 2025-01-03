using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        bool ValidateByLogin(string login);
        IQueryable<User> GetUsers();
    }
}
