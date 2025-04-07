namespace ApiPeliculas.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message, int errorCode = 403)
                : base(message, 403, errorCode)
        {
        }
    }
}
