using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Dal.Exceptions
{
    public class DatabaseContextException : Exception
    {
        public DatabaseContextException(string message) : base(message)
        {

        }
    }
}