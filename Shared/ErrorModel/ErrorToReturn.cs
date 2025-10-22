namespace Shared.ErrorModel
{
    public class ErrorToReturn
    {
        public int StatusCode {  get; set; }
        public string ErrorMessage { get; set; } = null!;
    }
}
