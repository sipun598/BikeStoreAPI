﻿using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Brands
    {
        public Brands()
        {
            Products = new HashSet<Products>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}