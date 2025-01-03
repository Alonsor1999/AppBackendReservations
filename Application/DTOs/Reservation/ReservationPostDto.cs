using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Reservation
{
    public class ReservationPostDto
    {
        // Identificador del espacio donde se va a realizar la reserva
        public Guid SpaceId { get; set; }
        public Guid UserId { get; set; }  // ID del usuario que hace la reserva
        public DateTime StartDate { get; set; }  // Fecha de inicio de la reserva
        public DateTime EndDate { get; set; }    // Fecha de fin de la reserva
    }
}
