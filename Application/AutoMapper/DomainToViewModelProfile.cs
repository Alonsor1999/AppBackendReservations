using Application.DTOs.Reservation;
using Application.DTOs.Space;
using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Reservation;
using Domain.Models.Role;
using Domain.Models.Space;
using Domain.Models.User;

namespace Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserPostDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Reservation, ReservationPostDto>();
            CreateMap<Space, SpaceDto>();
            CreateMap<Space, SpacePostDto>();

        }
    }
}
