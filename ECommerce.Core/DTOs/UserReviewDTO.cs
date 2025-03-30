using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class UserReviewDTO
    {

        public decimal Stars { get; set; } = 0;

        public string ReviewText { get; set; }

        public string ProductID { get; set; }

    }
}
