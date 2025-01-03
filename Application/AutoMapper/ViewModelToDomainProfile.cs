using Application.DTOs.Reservation;
using Application.DTOs.Space;
using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models.Reservation;
using Domain.Models.Space;
using Domain.Models.Role;
using Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserPostDto, User>();
            CreateMap<RoleDto, Role>();
            CreateMap<ReservationDto, Reservation>();
            CreateMap<ReservationPostDto, Reservation>();
            CreateMap<SpaceDto, Space>();
            CreateMap<SpacePostDto, Space>();
        }
    }
}
