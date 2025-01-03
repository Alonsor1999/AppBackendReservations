using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public class Reservation : Entity.Entity
    {
        public Guid SpaceId { get; set; }  // Relación con Space
        public Guid UserId { get; set; }

        // Fecha de inicio de la reserva
        public DateTime StartDate { get; set; }

        // Fecha de fin de la reserva
        public DateTime EndDate { get; set; }

        // Estado de la reserva (activo o cancelado)
        public bool Status { get; set; } = true;

        // Relación con el usuario que hizo la reserva
        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        // Relación con el espacio de la reserva
        [ForeignKey("SpaceId")]
        public Space.Space? Space { get; set; }
    }
}
