
using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Order_Module;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTOS.BasketDTo;
using Stripe;
using ProductClass = DomainLayer.Models.Product.Product;


namespace Service
{
    public class PaymentService(IConfiguration _configuration,IBaskerRepository _baskerRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string BasketId)
        {
            //Install Package Strip
            StripeConfiguration.ApiKey = _configuration["StripSection:SecretKey"];
            //Get Basket by BasketId
             var Basket =await _baskerRepository.GetBasketAsync(BasketId)
                ??throw new BasketNotFoundException(BasketId);
            //Get Amount(GetProduct + delivery methode Cost)
            var ProductRepo = _unitOfWork.GetRepository<ProductClass, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(int.Parse(item.Id))??
                   throw new ProductNotFoundException(int.Parse(item.Id)) ;
                item.Price=Product.Price;
                 
            }
            //DeliveryMethod
            ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ??throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);
            Basket.ShippingPrice = DeliveryMethod.Cost;
            var BaskentAmount = (long)(Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Cost) * 100;
            //Create Payment Intent[Create-Update]
            var PaymentService = new PaymentIntentService();
            if(Basket.PaymentIntentId is null)//Create
            {
                var Options=new PaymentIntentCreateOptions()
                {
                    Amount=BaskentAmount,
                    Currency="USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.PaymentIntentId=PaymentIntent.Id;
                Basket.ClientSecret=PaymentIntent.ClientSecret; 
                
            }
            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = BaskentAmount,

                };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, options);

            }
            await _baskerRepository.CreateOrUpadateBasketAsync(Basket);
            return _mapper.Map<BasketDto>(Basket);
        }

        //public async Task UpdateOrderPaymentStatus(string Request, string stripHeader)
        //{
        //    //webhook secret
        //    var EndPoint = _configuration.GetSection("StripSection")["EndPointSecret"];
        //    //All information about event that was sent by stripe
        //    var stripeEvent=EventUtility.ConstructEvent(Request,stripHeader,EndPoint);
        //    //that mean payment completed
        //    var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;
        //    switch (stripeEvent.Type)
        //    {
        //        case "payment_intent.payment_failed":
        //                await UpdatePaymentFailed(PaymentIntent!.Id);
        //            break;
        //        case "payment_intent.succeeded":
        //            await UpdatePaymentReceived(PaymentIntent!.Id);
        //            break;
        //        default:
        //            Console.WriteLine("Unhandled event type:{0}", stripeEvent.Type);
        //            break;
        //    }
        //}

        //private async Task UpdatePaymentReceived(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //private async Task UpdatePaymentFailed(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
