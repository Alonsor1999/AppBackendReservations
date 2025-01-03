using Application.Common.Response;
using Application.Cqrs.Reservation.Commands;
using Application.Cqrs.Reservation.Queries;
using Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reservation
{
    public interface IReservationService
    {
        // Método para crear una nueva reserva
        Task<ApiResponse<ReservationDto>> CreateReservation(PostReservationCommand request);

        // Método para cancelar una reserva
        Task<ApiResponse<bool>> CancelReservation(DeleteReservationCommand request);

        // Método para listar todas las reservas sin filtros
        Task<ApiResponse<List<ReservationDto>>> GetReservations(GetReservationsQuery query);
    }
}
