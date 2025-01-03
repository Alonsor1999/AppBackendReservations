using Domain.Models.Role;
using Domain.Models.User;
using Domain.Models.Reservation;
using Domain.Models.Space;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Reservation> ReservationRepository { get; }
        IRepository<Space> SpaceRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
        //string GetDbConnection();
    }
}
