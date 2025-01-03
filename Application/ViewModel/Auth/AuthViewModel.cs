using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.Auth
{
    public class AuthViewModel
    {
        public string? token { get; set; }
        public UserDto? user { get; set; }
        //public string? IpRemote { get; set; }

    }
}
