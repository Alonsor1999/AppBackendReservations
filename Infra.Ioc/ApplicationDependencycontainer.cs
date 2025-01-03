using Application.Interfaces.Auths;
using Application.Interfaces.EncryptAndDecrypt;
using Application.Interfaces.PageCredentials;
using Application.Interfaces.Reservation;
using Application.Interfaces.User;
using Application.Interfaces.Space;
using Application.Services.Auths;
using Application.Services.EncryptAndDecrypt;
using Application.Services.PageCredentials;
using Application.Services.Reservation;
using Application.Services.User;
using Application.Services.Space;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public static class ApplicationDependencycontainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Auth
            services.AddScoped<IAuthService, AuthService>();
            // User
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IRolService, RolService>();
            // Password
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // PageCredentials
            services.AddScoped<IPageCredentialsService, PageCredentialsService>();
            // EncryptAndDecrypt
            services.AddScoped<IEncryptAndDecryptService, EncryptAndDecryptService>();
            // Reservation
            services.AddScoped<IReservationService, ReservationService>();
            //Space
            services.AddScoped<ISpaceService, SpaceService>();




        }
    }
}
