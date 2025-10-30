namespace DomainLayer.Exceptions
{
    public sealed class UnauthorizeException(string Message="Invalid Email Or Passward"):Exception(Message)
    {
    }
}
