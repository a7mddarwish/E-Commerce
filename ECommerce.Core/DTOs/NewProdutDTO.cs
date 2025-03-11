using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class NewProdutDTO 
    {
        public int categoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short availInStock { get; set; }
        public string category { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile>? formFile { get; set; }


    }
}
