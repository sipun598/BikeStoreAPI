using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class BrandProductsDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductIdNameDto> Products { get; set; }
    }
}