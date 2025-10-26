namespace DomainLayer.Models.Product
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }//PK
    }
}
