using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System.Text;

namespace Presintation.Attributes
{
    public class Cash(int DurationInSecond=90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create CashKey 
            string CashKey = CreateCashKey(context.HttpContext.Request);
            //Search For value With Cash Key
            ICashService CashService=context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var Cashvalue=await CashService.GetAsync(CashKey);
            // Return value if not null
            if (Cashvalue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = Cashvalue,
                    ContentType="application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return ;
            }


            //InvokeNext
            var ExecutedContext=await next.Invoke();
            //Set Value With CashValue
            if (ExecutedContext.Result is OkObjectResult result)
            { 
                await CashService.SetAsync(CashKey,result.Value!,TimeSpan.FromSeconds(DurationInSecond));
            }
        }

        private string CreateCashKey(HttpRequest request)
        {
            //BaseUrl/api/Products?typeid=10?brandid=2
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            { 
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
