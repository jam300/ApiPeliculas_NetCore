namespace ApiPeliculas.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, int errorCode = 401)
        : base(message, 401, errorCode)
        {
        }
    }
}
