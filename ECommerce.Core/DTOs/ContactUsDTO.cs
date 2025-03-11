using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class ContactUsDTO
    {

        public string name { get ; set; } 
        public string email { get; set; }
        public string phone { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

    }
}
