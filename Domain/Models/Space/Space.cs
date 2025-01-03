using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Space
{
    public class Space : Entity.Entity
    {
        public string Name { get; set; }
        public string SpaceDescription { get; set; }
    }
}
