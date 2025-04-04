﻿using ECommerce.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class ProductDTO
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public short availInStock { get; set; }
        public decimal Price { get; set; }
        public AvgReviewsDTO Reviewsinfo { get; set; }


        public ICollection<Image> Images { get; set; }

    }
}
