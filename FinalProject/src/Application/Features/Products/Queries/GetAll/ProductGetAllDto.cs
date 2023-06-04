﻿namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllDto
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public bool IsOnSale { get; set; }
        public decimal? SalePrice { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Id { get; internal set; }
    }
}
