using System;
namespace LightNote.Domain.Exceptions
{
	public class InvalidLengthException : Exception
	{
		public InvalidLengthException(string property, int min, int max) : base($"{property} length is required {min} to {max} characters")
		{
		}
	}
}

