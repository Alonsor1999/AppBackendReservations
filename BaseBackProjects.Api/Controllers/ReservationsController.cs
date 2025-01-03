using Application.Common.Response;
using Application.Cqrs.Reservation.Commands;
using Application.Cqrs.Reservation.Queries;
using Application.Interfaces.Reservation;
using BaseBackProjects.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OxxoBrasil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReservationController : ApiControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Crear una nueva reserva
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] PostReservationCommand command)
        {
            var response = await _reservationService.CreateReservation(command);

            if (response.Result)
                return Ok(response);
            else
                return BadRequest(response);
        }

        /// <summary>
        /// Cancelar una reserva por ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> CancelReservation(Guid Id)
        {
            var response = await _reservationService.CancelReservation(new DeleteReservationCommand() { Id = Id });

            if (response.Result)
                return Ok(response);
            else
                return BadRequest(response);
        }

        /// <summary>
        /// Obtener todas las reservas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetReservations([FromQuery] GetReservationsQuery query)
        {
            var response = await _reservationService.GetReservations(query);

            if (response.Result)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
