namespace DomainLayer.Constracts
{
    public interface ICashRepository
    {
        //Get
        Task<string?>GetAsync(string CashKey);
        //Set
        Task SetAsync(string CashKey, string Cashvalue,TimeSpan TimeToLive);
    }
}
