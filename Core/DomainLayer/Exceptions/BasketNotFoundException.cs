namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundException(string id):NotFoundExceptions($"Basket With Id={id} Is Not Found")
    {
    }
}
