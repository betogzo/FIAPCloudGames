namespace FIAP.CloudGames.Domain.Shared
{
    public class Result
    {
        public bool IsSuccess      { get; }
        public IReadOnlyList<string> Errors { get; }

        protected Result(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors    = errors.ToList().AsReadOnly();
        }

        public static Result Ok()
            => new Result(true, []);

        public static Result Fail(IEnumerable<string> errors)
            => new Result(false, errors);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(T data)
            : base(true, [])
        {
            Data = data;
        }

        private Result(IEnumerable<string> errors)
            : base(false, errors)
        {
        }

        public static Result<T> Ok(T value)
            => new Result<T>(value);

        public new static Result<T> Fail(IEnumerable<string> errors)
            => new Result<T>(errors);
    }
}