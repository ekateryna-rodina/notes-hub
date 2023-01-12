using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Application.Models
{
    public class AuthenticatedResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}