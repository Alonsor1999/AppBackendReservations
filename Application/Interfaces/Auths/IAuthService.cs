using Application.Cqrs.Auth.Commands;
using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auths
{
    public interface IAuthService
    {
        Task<UserDto> GetUserByLogin(string login);
        //Task<AuthViewModel> GetAuth(PostLoginAuth auth);
        Task<AuthViewModel> GetAuth(PostLoginCommand auth);
        Task<UserDto> GetUserById(Guid? Id);
    }
}
