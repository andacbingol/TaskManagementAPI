using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementAPI.Application.DTOs.Authentication
{
    public class LoginUserDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
