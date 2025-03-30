using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts.Email
{
    public interface IEmailSender
    {
        public Task EmailFromContactUs(ContactUsDTO contactus);
        public Task SendConfirmationEmail(string email, AppUser user, string? token, string confirmationLink , string message);
        public Task SendVarificationEmail(string email, AppUser user, string? token, string confirmationLink);
        public Task SendResetPasswoedEmail(string email, AppUser user, string? token, string confirmationLink);
    }
}
