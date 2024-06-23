namespace Backend;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string msg):base(msg)
    {

    }
}