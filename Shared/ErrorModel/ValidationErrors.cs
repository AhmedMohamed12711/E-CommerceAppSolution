namespace Shared.ErrorModel
{
    public class ValidationErrors
    {
        public string Field { get; set; } = null!;
        public IEnumerable<string> Errors { get; set; } = [];

    }
}
