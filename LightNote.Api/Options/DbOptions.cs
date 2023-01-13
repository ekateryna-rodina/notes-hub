using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Api.Options
{
    public class DbOptions
    {
        public string DbUser { get; set; } = default!;
        public string DbPassword { get; set; } = default!;
        public string DbPort { get; set; } = default!;
        public string DbName { get; set; } = default!;
    }
}

