using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOs
{
    public class RegisterDTO
    {

       
        public string FullName { get; set; }

        public string Email { get; set; }

     //   public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
    }
}
