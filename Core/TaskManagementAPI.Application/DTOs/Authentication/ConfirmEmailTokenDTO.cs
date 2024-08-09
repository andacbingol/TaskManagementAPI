using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementAPI.Application.DTOs.Authentication
{
    public class ConfirmEmailTokenDTO
    {
        public Guid Id { get; set; }
        public string ConfirmEmailToken { get; set; }
    }
}
