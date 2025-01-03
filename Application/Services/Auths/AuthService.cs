using Application.Common.Auth;
using Application.Common.Exceptions;
using Application.Cqrs.Auth.Commands;
using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Interfaces.Auths;
using Application.ViewModel.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;

namespace Application.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _autoMapper;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IMapper autoMapper,
            IAuthRepository authRepository,
            IPasswordHasher passwordHasher
        )
        {
            _authRepository = authRepository;
            _autoMapper = autoMapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto?> GetUserByLogin(string login)
        {
            return _authRepository
                    .GetUsers()
                    .Where(c => c.Login == login && c.Status == true)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefault();
        }

        public async Task<UserDto?> GetUserById(Guid? Id)
        {
            return _authRepository
                    .GetUsers()
                    .Where(c => c.Id == Id)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefault();
        }

        public async Task<AuthViewModel> GetAuth(PostLoginCommand auth)
        {


            var userDto = _authRepository
                            .GetUsers()
                            .Where(x => x.Document.Trim() == auth.Document.Trim())
                            .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                            .FirstOrDefault();

            var isValid = false;
            if (userDto != null)
            {
                isValid = _passwordHasher.Check(userDto.Password, auth.Password);
            }

            if (!isValid)
            {
                throw new UnAuthorizeException("Usuario o contraseña incorrecta");
            }

            var authViewModel = new AuthViewModel()
            {
                user = userDto,
                token = GetAuthToken(userDto)
            };

            return new AuthViewModel()
            {
                user = userDto,
                token = GetAuthToken(userDto)
            };
        }

        public string GetAuthToken(UserDto user)
        {
            AuthToken auth = new AuthToken();
            return auth.GenerateToken(user);
        }
    }
}
