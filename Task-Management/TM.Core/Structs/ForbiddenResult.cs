namespace TM.Core.Structs
{
    public class ForbiddenResult : ErrorResult
    {
        public ForbiddenResult(string message) : base(message)
        {
        }

        public ForbiddenResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }

    public class ForbiddenResult<T> : ErrorResult<T>
    {
        public ForbiddenResult(string message) : base(message)
        {
        }

        public ForbiddenResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
        {
        }
    }
}
