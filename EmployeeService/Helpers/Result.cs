namespace EmployeeService.Helpers
{
    public record Result
    {
        protected Result(string message, bool status)
        {
            Succeeded = status;
            Message = message;
        }

        public string Message { get; set; }
        public bool Succeeded { get; set; }

        public static Result Success(bool status = true, string message = null) => new(message, status);

        public static Result<T> Success<T>(T data, string message = null) => new(data, message, true);

        public static Result<T> Failure<T>(string message = null) => new(default(T), message, false);

        public static Result Failure(string message = null) => new(message, false);
    }

    public record Result<T> : Result
    {
        public Result(T data, string message, bool status) : base(message, status)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
