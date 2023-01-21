using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Application.Exceptions
{
    public class IdentityDoesNotExistException : Exception
    {
        public IdentityDoesNotExistException(string message) : base(message)
        {
        }
    }
}