using System.Collections.Generic;

namespace Application.DTOs
{
    public class StorePaginationViewModel : Pagination
    {
        public List<StoresDto> Items { get; set; }
    }
}