using System;
namespace LightNote.Application.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException(string message) : base(message) { }
    }
}

