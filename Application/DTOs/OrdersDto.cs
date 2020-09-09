﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.OrderDtos
{
    public class OrdersDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }


    }
}
