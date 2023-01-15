using System;
namespace LightNote.Application.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resource) : base($"{resource} not found") { }
    }
}

