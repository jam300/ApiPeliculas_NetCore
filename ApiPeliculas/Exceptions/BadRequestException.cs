namespace ApiPeliculas.Exceptions
{
    [Serializable]
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, int errorCode = 400)
            : base(message, 400, errorCode)
        {
        }

    }
}
