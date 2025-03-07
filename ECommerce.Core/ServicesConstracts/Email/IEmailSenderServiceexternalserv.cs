using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServicesConstracts.Email
{
    public interface IEmailSenderServiceexternalserv
    {
        public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false);
    }
}
