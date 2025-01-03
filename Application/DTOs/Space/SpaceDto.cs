using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Space
{
    public class SpaceDto
    {
        public Guid Id { get; set; }              // Identificador del Espacio
        public string Name { get; set; }
        public string SpaceDescription { get; set; }
    }
}
