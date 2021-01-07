using System.Collections.Generic;

namespace Application.DTOs
{
    public class BrandPaginationViewModel : Pagination
    {
        public List<BrandsDto> Items { get; set; }
    }
}