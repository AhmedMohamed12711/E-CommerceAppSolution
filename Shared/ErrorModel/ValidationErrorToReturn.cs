using System.Net;
using System.Text.Json.Serialization;

namespace Shared.ErrorModel
{
    public class ValidationErrorToReturn
    {
        public int StatusCode{get;set;}=(int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Failed";
        public IEnumerable<ValidationErrors> ValidationErrors { get; set; } = [];

    }
}
