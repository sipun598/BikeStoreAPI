using Application.DTOs.CustomerDtos;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class CustomerPaginationViewModel : Pagination
    {
        public List<CustomersDto> Items { get; set; }
    }
}