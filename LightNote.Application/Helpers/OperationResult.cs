using System;
namespace LightNote.Application.Helpers
{
	public class OperationResult<T>
	{
		private OperationResult()
		{
		}

		public bool IsSuccess { get; private set; } = default!;
		public T Value { get; private set; } = default!;
        public IEnumerable<Exception> Exceptions { get; private set; } = default!;

		public static OperationResult<T> CreateSuccess(T value) {
			return new OperationResult<T> {
				IsSuccess = true,
				Value = value
            };
		}

        public static OperationResult<T> CreateFailure(IEnumerable<Exception> exs)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Exceptions = exs
            };
        }
    }
}

