namespace ApiPeliculas.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public int StatusCode { get; }
        public int ErrorCode { get; }

        protected BaseException(string message, int statusCode, int errorCode = 0) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        protected BaseException(string message, int statusCode, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
