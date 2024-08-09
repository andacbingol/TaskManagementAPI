using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementAPI.Infrastructure
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int AccessTokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInMinutes { get; set; }
    }
}
