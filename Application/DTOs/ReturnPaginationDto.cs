using System.Collections.Generic;

namespace Application.DTOs
{
    public static class ReturnPaginationDto
    {
        public static PaginationViewModel<T> GetPage<T>(IEnumerable<T> list, int pageNumber, int itemsPerPage, int count)
        {
            PaginationViewModel<T> res = new PaginationViewModel<T>() { ItemsPerPage = itemsPerPage };
            List<T> pageItemList = new List<T>();

            res.TotalItemsCount = count;
            if (res.TotalItemsCount > 0)
            {
                res.PageCount = res.TotalItemsCount / res.ItemsPerPage + (res.TotalItemsCount % res.ItemsPerPage == 0 ? 0 : 1);
                res.PageIndex = pageNumber > res.PageCount - 1 ? res.PageCount - 1 : pageNumber;
                pageItemList.AddRange(list);
            }
            else
            {
                res.PageCount = 0;
                res.PageIndex = 0;
            }
            res.Items = pageItemList;

            return res;
        }
    }
}