using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts
{
    public interface ITokenProvider
    {
        public siginedinwithtokenDTO GenerateJWTToken(AppUser user , IList<string>Roles);

    }
}
