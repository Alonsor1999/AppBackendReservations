using Application.Common.Response;
using Application.Cqrs.Reservation.Commands;
using Application.DTOs.Reservation;
using Application.Interfaces.Reservation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cqrs.Reservation.Commands
{
    public class PostReservationCommand : IRequest<ApiResponse<ReservationDto>>
    {
        // El DTO que contiene los datos necesarios para la creación de la reserva
        public ReservationPostDto ReservationPostDto { get; set; }
    }
    public class PostReservationCommandHandler : IRequestHandler<PostReservationCommand, ApiResponse<ReservationDto>>
    {
        private readonly IReservationService _reservationService;
        public PostReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<ApiResponse<ReservationDto>> Handle(PostReservationCommand request, CancellationToken cancellationToken)
        {
            return await _reservationService.CreateReservation(request);
        }
    }
}
