using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Application.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("Token is invalid") { }
    }
}