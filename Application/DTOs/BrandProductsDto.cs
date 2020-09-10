using System.Collections.Generic;

namespace Application.DTOs
{
    public class BrandProductsDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductIdNameDto> Products { get; set; }
    }
}