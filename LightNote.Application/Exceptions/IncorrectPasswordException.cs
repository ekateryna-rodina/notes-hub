using System;
namespace LightNote.Application.Exceptions
{
	public class IncorrectPasswordException : Exception
	{
        public IncorrectPasswordException(string message) : base(message) { }
    }
}

