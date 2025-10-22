namespace DomainLayer.Exceptions
{
    public class ProductNotFoundException(int id):NotFoundExceptions($"The Product With Id={id} Is Not Found")
    {
    }
}
