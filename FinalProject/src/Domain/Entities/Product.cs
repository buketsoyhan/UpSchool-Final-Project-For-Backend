using Domain.Common;

namespace Domain.Entities
{
    public class Product : EntityBase<Guid>
    {
        public Guid Id { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public bool IsOnSale { get; set; }
        public decimal? SalePrice { get; set; }

    }
}
