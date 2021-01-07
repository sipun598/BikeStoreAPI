using System.Collections.Generic;

namespace Application.DTOs
{
    public class ProductsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }

    public class ProductPaginationViewModel : Pagination
    {
        public List<ProductsDto> Items { get; set; }
    }
}