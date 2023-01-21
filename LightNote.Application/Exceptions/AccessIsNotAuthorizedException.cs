namespace LightNote.Application.Exceptions
{
    public class AccessIsNotAuthorizedException : Exception
    {
        public AccessIsNotAuthorizedException() : base("Access is not authorized")
        {

        }
    }
}