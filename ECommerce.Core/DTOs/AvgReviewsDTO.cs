using ECommerce.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class AvgReviewsDTO
    {   

        public string ProductID { get; set; }

        public decimal avgStars { get; set; }

        public int ReviewsNum { get; set; }

    }
}
