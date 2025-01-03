using Application.Common.Response;
using Application.Cqrs.Reservation.Commands;
using Application.Cqrs.Reservation.Queries;
using Application.DTOs.Reservation;
using Application.DTOs.User;
using Application.Interfaces.Reservation;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        // Constructor donde se inyectan las dependencias
        public ReservationService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        // Implementación de método para crear una reserva
        public async Task<ApiResponse<ReservationDto>> CreateReservation(PostReservationCommand request)
        {
            var response = new ApiResponse<ReservationDto>();

            try
            {
                // 1. Validar que las fechas sean correctas
                if (request.ReservationPostDto.StartDate >= request.ReservationPostDto.EndDate)
                {
                    response.Result = false;
                    response.Message = "La fecha de inicio debe ser anterior a la fecha de fin.";
                    return response;
                }

                // 2. Validar el tiempo mínimo y máximo de la reserva
                var minReservationDuration = TimeSpan.FromHours(1); // Ejemplo: 1 hora mínima
                var maxReservationDuration = TimeSpan.FromDays(3); // Ejemplo: 3 días máximo
                var reservationDuration = request.ReservationPostDto.EndDate - request.ReservationPostDto.StartDate;

                if (reservationDuration < minReservationDuration || reservationDuration > maxReservationDuration)
                {
                    response.Result = false;
                    response.Message = $"La duración de la reserva debe ser entre {minReservationDuration.TotalHours} horas y {maxReservationDuration.TotalDays} días.";
                    return response;
                }

                // 3. Validar solapamiento de reservas en el mismo espacio y rango de fechas
                var existingReservation = await _unitOfWork.ReservationRepository
                    .Get()
                    .Where(r => r.SpaceId == request.ReservationPostDto.SpaceId &&
                                r.StartDate < request.ReservationPostDto.EndDate &&
                                r.EndDate > request.ReservationPostDto.StartDate)
                    .FirstOrDefaultAsync();

                if (existingReservation != null)
                {
                    response.Result = false;
                    response.Message = "Ya existe una reserva que se solapa con el rango de fechas para este espacio.";
                    return response;
                }

                // 4. Validar que el usuario no tiene otra reserva en el mismo tiempo
                var overlappingUserReservation = await _unitOfWork.ReservationRepository
                    .Get()
                    .Where(r => r.UserId == request.ReservationPostDto.UserId &&
                                r.StartDate < request.ReservationPostDto.EndDate &&
                                r.EndDate > request.ReservationPostDto.StartDate)
                    .FirstOrDefaultAsync();

                if (overlappingUserReservation != null)
                {
                    response.Result = false;
                    response.Message = "El usuario ya tiene una reserva en el mismo rango de fechas.";
                    return response;
                }

                // 5. Mapeo de DTO recibido a entidad de dominio
                var reservation = _autoMapper.Map<Domain.Models.Reservation.Reservation>(request.ReservationPostDto);

                // 6. Obtener el nombre del espacio desde el repositorio
                var space = await _unitOfWork.SpaceRepository.GetById(request.ReservationPostDto.SpaceId);
                if (space == null)
                {
                    response.Result = false;
                    response.Message = "El espacio no existe.";
                    return response;
                }

                // 7. Obtener el usuario asociado al UserId
                var user = await _unitOfWork.UserRepository.GetById(request.ReservationPostDto.UserId);
                if (user == null)
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                    return response;
                }

                // 8. Guardar la nueva reserva
                var createdReservation = await _unitOfWork.ReservationRepository.Add(reservation);

                // 9. Mapeo de la entidad de dominio a DTO
                var reservationDto = _autoMapper.Map<ReservationDto>(createdReservation);
                reservationDto.SpaceName = space.Name;
                reservationDto.UserName = user.Names; // Asumimos que `Name` es el nombre del usuario

                response.Data = reservationDto;
                response.Result = true;
                response.Message = "Reserva creada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al crear la reserva: {ex.Message}";
            }

            return response;
        }

        // Implementación de método para cancelar una reserva
        public async Task<ApiResponse<bool>> CancelReservation(DeleteReservationCommand request)
        {
            var response = new ApiResponse<bool>();

            try
            {
                // Buscar la reserva por Id
                var reservation = await _unitOfWork.ReservationRepository.GetById(request.Id);

                if (reservation == null)
                {
                    response.Result = false;
                    response.Message = "Reserva no encontrada.";
                    response.Data = false;
                    return response;
                }

                // Eliminar la reserva
                var result = await _unitOfWork.ReservationRepository.Delete(reservation);

                response.Result = result;
                response.Data = result;
                response.Message = result ? "Reserva cancelada exitosamente." : "Error al cancelar la reserva.";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al cancelar la reserva: {ex.Message}";
                response.Data = false;
            }

            return response;
        }

        // Método para obtener todas las reservas sin filtros
        public async Task<ApiResponse<List<ReservationDto>>> GetReservations(GetReservationsQuery query)
        {
            var response = new ApiResponse<List<ReservationDto>>();

            try
            {
                // Obtén el IQueryable de las reservas, con los filtros aplicados si se proporcionan
                var reservationsQuery = _unitOfWork.ReservationRepository.Get();

                // Filtro por SpaceId, si se proporciona
                if (query.SpaceId.HasValue)
                {
                    reservationsQuery = reservationsQuery.Where(r => r.SpaceId == query.SpaceId.Value);
                }

                // Filtro por UserId, si se proporciona
                if (query.UserId.HasValue)
                {
                    reservationsQuery = reservationsQuery.Where(r => r.UserId == query.UserId.Value);
                }

                // Filtro por StartDate, si se proporciona
                if (query.StartDate.HasValue)
                {
                    reservationsQuery = reservationsQuery.Where(r => r.StartDate >= query.StartDate.Value);
                }

                // Filtro por EndDate, si se proporciona
                if (query.EndDate.HasValue)
                {
                    reservationsQuery = reservationsQuery.Where(r => r.EndDate <= query.EndDate.Value);
                }

                // Ejecutar la consulta
                var reservations = await reservationsQuery.ToListAsync();

                // Verificar si se encontraron reservas
                if (reservations == null || !reservations.Any())
                {
                    response.Data = new List<ReservationDto>();  // Asignar lista vacía
                    response.Result = false;  // Indicar que no se encontraron reservas
                    response.Message = "No se encontraron reservas que coincidan con los filtros especificados.";  // Mensaje de no encontrado
                }
                else
                {
                    // Mapear las reservas a DTO
                    var reservationDtos = _autoMapper.Map<List<ReservationDto>>(reservations);

                    // Asignar nombre de espacio y usuario para cada reserva
                    foreach (var reservationDto in reservationDtos)
                    {
                        var space = await _unitOfWork.SpaceRepository.GetById(reservationDto.SpaceId);
                        var user = await _unitOfWork.UserRepository.GetById(reservationDto.UserId);
                        reservationDto.SpaceName = space.Name;  // Asignamos el nombre del espacio
                        reservationDto.UserName = user.Names;  // Asignamos el nombre del usuario
                    }

                    // Devolver las reservas mapeadas con los nombres
                    response.Data = reservationDtos;
                    response.Result = true;
                    response.Message = "Reservas obtenidas exitosamente.";
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al obtener las reservas: {ex.Message}";
            }

            return response;
        }
    }
}
