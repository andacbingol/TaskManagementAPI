using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementAPI.Application.DTOs.User
{
    public class UpdatePasswordDTO
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordNewConfirm { get; set; }
    }
}
