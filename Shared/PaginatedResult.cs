
namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int _PageSize, int _PageIndex, int _totalCount,IEnumerable<TEntity> _Data)
        {
            PageSize = _PageSize;
            PageIndex = _PageIndex;
            count = _totalCount;
            data = _Data;
        }
        public int PageSize {  get; set; }
        public int PageIndex{ get; set; }
        public int count { get; set; }
        public IEnumerable<TEntity> data { get; set; }
    }
}
