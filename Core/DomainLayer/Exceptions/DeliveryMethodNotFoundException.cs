
namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id):NotFoundExceptions($"No DeliveryMethod Was Found For This Id:{id}")
    {
    }
}
