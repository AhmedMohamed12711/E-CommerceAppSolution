namespace DomainLayer.Exceptions
{
    public sealed class UserNotFoundException(string Email):NotFoundExceptions($"User With the Email:{Email} is Not Found")
    {
    }
}
