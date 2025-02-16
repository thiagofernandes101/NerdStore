namespace NerdStore.Core.DomainObjects
{
    public class Result<T> where T : notnull
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public T? Value { get; }

        private Result(bool isSuccess, string? error, T? value)
        {
            if (isSuccess && value == null && !typeof(T).IsValueType)
                throw new ArgumentException("Success result must contain a value.", nameof(value));

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("Failure result must contain an error message.", nameof(error));

            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, null, value);

        public static Result<T> Failure(string error) => new(false, error, default);
    }
}
