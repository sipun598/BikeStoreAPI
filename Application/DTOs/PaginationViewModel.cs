using System.Collections.Generic;

namespace Application.DTOs
{
    public class PaginationViewModel<T>
    {
        public int PageCount { get; set; }
        public int TotalItemsCount { get; set; }
        public int PageIndex { get; set; }
        public int ItemsPerPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}