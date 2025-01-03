using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PageCredentials
{
    public class PageCredentialsServiceDto
    {
        public int IdApplication { get; set; }
        public string? AppName { get; set; }
        public string? User { get; set; }
        public string? UserWebApp { get; set; }
        public string? Link { get; set; }
        public string? Password { get; set; }
    }
}
