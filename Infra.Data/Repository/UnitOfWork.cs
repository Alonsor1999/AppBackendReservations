using Domain.Interfaces;
using Domain.Models.Role;
using Domain.Models.User;
using Infra.Data.Context;
using Domain.Models.Reservation;
using Domain.Models.Space;

namespace Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDBContext _ctx;
        public IRepository<Role> RoleRepository => new BaseRepository<Role>(_ctx);
        public IRepository<User> UserRepository => new BaseRepository<User>(_ctx);
        public IRepository<Reservation> ReservationRepository => new BaseRepository<Reservation>(_ctx);
        public IRepository<Space> SpaceRepository => new BaseRepository<Space>(_ctx);

        public UnitOfWork(AplicationDBContext ctx)
        {
            _ctx = ctx;

        }

        //public string GetDbConnection()
        //{
        //    return _ctx.Database.GetDbConnection().ConnectionString;
        //}

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
