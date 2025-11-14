using Shared.DTOS.BasketDTo;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentAsync(string BasketId);
        //public Task UpdateOrderPaymentStatus(string Request, string stripHeader);
    }
}
