using Application.Common.Interfaces;
using Domain.Models.Entity;
using Domain.Models.Reservation;
using Domain.Models.Role;
using Domain.Models.Space;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class AplicationDBContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AplicationDBContext(DbContextOptions options, 
                                ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Role> Rols { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Space> Spaces { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var UserInfo = _currentUserService.GetUserInfo();
            var userName = string.Concat(UserInfo.Name, " ", UserInfo.LastName);

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Id;
                        entry.Entity.CreatedByName = userName;
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.Id;
                        entry.Entity.UpdatedByName = userName;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = DateTime.Now;
                        entry.Entity.UpdatedByName = userName;
                        entry.Entity.UpdatedBy = _currentUserService.Id;
                        break;
                }
            }


            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
