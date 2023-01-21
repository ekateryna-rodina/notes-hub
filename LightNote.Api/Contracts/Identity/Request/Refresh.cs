using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Api.Contracts.Identity.Request
{
    public class Refresh
    {
        [Required]
        public string RefreshToken { get; set; } = default!;
    }
}