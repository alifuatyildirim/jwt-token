using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtToken.API.Jwt
{
    public class JwtTokenConfiguration
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        /// <summary>
        /// dakika cinsinden token expire süresi
        /// </summary>
        public int Expires { get; set; } = 30;
    }
}
