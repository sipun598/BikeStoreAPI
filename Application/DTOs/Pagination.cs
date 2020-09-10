namespace Application.DTOs
{
    public class Pagination
    {
        public int PageCount { get; set; }
        public int TotalItemsCount { get; set; }
        public int PageIndex { get; set; }
        public int ItemsPerPage { get; set; }
    }
}