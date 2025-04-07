namespace ApiPeliculas.Exceptions
{
    [Serializable]
    public class NotFoundException: BaseException
    {
        public NotFoundException(string message, int errorCode = 404)
            : base(message, 404, errorCode)
        {
        }

    }
}
