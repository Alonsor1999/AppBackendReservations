using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.User;
using Application.Interfaces.User;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUnitOfWork unitOfWork, IMapper autoMapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<List<UserDto>>> GetUsers()
        {
            var response = new ApiResponse<List<UserDto>>();

            try
            {
                response.Data = _autoMapper.Map<List<UserDto>>(_unitOfWork.UserRepository.Get().ToList());
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }

        public async Task<ApiResponse<UserDto>> GetUsersById(GetUsersQueryById request)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.GetById(request.Id));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }


        public async Task<ApiResponse<UserDto>> AddUser(PostUserCommand request)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var User = _autoMapper.Map<Domain.Models.User.User>(request.UserPostDto);
                User.Password = _passwordHasher.Hash(User.Password);

                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.Add(User));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear Usuario. {ex.Message} ";
            }

            return response;
        }

        public async Task<ApiResponse<UserDto>> UpdateUser(PutUserCommand request)
        {
            var response = new ApiResponse<UserDto>();
            try
            {
                request.UserDto.Password = _passwordHasher.Hash(request.UserDto.Password);
                var userDto = new UserDto
                {
                    Id = request.UserDto.Id,
                    Email = request.UserDto.Email,
                    Names = request.UserDto.Names,
                    Password = request.UserDto.Password,
                    Login = request.UserDto.Login,
                    RoleId = request.UserDto.RoleId,
                    Document = request.UserDto.Document,
                    Phone1 = request.UserDto.Phone1
                };

                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.Put(_autoMapper.Map<Domain.Models.User.User>(userDto)));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. {ex.Message} ";
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteUser(DeleteUserCommand request)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var User = await _unitOfWork.UserRepository.GetById(request.Id);

                response.Data = await _unitOfWork.UserRepository.Delete(User);
                response.Result = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al eliminar el registro, consulte con el administrador. {ex.Message} ";
                throw;
            }
            return response;
        }
    }
}
