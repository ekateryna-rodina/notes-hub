using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Application.Exceptions
{
    public class EntityIsRequiredException : Exception
    {
        public EntityIsRequiredException(string entity) : base($"At least one {entity} is required")
        {

        }
    }
}