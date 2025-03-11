using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }


        [HttpPost("ContactUs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendContactUsEmail(ContactUsDTO contactus)
        {
            if (!ModelState.IsValid)
                return BadRequest(new {message = "Check your info agian"});

           await emailSender.EmailFromContactUs(contactus);

            return Ok("Email sent done");
                    
        }
    }
}
