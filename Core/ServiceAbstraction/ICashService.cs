namespace ServiceAbstraction
{
    public interface ICashService
    {
        Task<string?> GetAsync(string CashKey);
        Task SetAsync(string CashKey, object CashValue,TimeSpan TimeToLive);
    }
}
