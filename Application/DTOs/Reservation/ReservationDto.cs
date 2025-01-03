using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Reservation
{
    public class ReservationDto
    {
        public Guid Id { get; set; }              // Identificador de la reserva
        public Guid SpaceId { get; set; }         // Identificador del espacio
        public Guid UserId { get; set; }          // Identificador del usuario que hizo la reserva
        public string UserName { get; set; }      // Nombre del usuario
        public string SpaceName { get; set; }     // Nombre del espacio
        public DateTime StartDate { get; set; }   // Fecha de inicio de la reserva
        public DateTime EndDate { get; set; }     // Fecha de fin de la reserva
        public bool Status { get; set; }          // Estado de la reserva (activa o cancelada)
        public DateTime CreatedAt { get; set; }   // Fecha de creación de la reserva
        public DateTime UpdatedAt { get; set; }   // Fecha de la última actualización (si aplica)
    }

}
