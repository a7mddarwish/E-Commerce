using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class ChangeUserInfoDTO
    {
        public string passwordToConfirm {  get; set; } 

        public string Address { get; set; }

        public string FullName { get; set; }

    }
}
