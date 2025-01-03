using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.Auth
{
    public class AuthInBoundViewModel
    {
        public string? token { get; set; }
        public UserDto? user { get; set; }
        public string? message { get; set; }
        public string? code { get; set; }
        public bool status { get; set; }
    }
}
