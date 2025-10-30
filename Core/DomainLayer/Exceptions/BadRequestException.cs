namespace DomainLayer.Exceptions
{
    public class BadRequestException(List<string>error):Exception("Validation Failed")
    {
        public List<string> Errors {  get; }=error;
    }
}
