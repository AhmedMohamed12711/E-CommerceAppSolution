using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErrorModel;

namespace E_CommerceApp.Factories
{
    public static class APIesponseFactory
    {
        public static IActionResult GenerateApiValdiationErrorResponse(ActionContext context)
        {
            
                var Errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .Select(e => new ValidationErrors()
                    {
                        Field = e.Key,
                        Errors = e.Value.Errors.Select(er => er.ErrorMessage)
                    });
                var Respone = new ValidationErrorToReturn()
                {
                    ValidationErrors = Errors,

                };

                return new BadRequestObjectResult(Respone);

            
        }
    }
}
