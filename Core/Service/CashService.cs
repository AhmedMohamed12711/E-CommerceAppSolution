
using DomainLayer.Constracts;
using ServiceAbstraction;
using System.Text.Json;

namespace Service
{
    public class CashService(ICashRepository _cashRepository) : ICashService
    {
        public async Task<string?> GetAsync(string CashKey)=>await _cashRepository.GetAsync(CashKey);
       

        public async Task SetAsync(string CashKey, object CashValue, TimeSpan TimeToLive)
        {
            var Value =JsonSerializer.Serialize(CashValue);
            await _cashRepository.SetAsync(CashKey, Value, TimeToLive);
        }
    }
}
