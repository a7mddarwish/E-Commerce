using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts.Email;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ECommerce.Core.Services
{
    public class EmailSender :IEmailSender
    {
        private readonly IEmailSenderServiceexternalserv sender;

        public EmailSender(IEmailSenderServiceexternalserv emailsender)
        {
            this.sender = emailsender;
        }

        public async Task SendConfirmationEmail(string email, AppUser user , string?token , string confirmationLink)
        {
            // Generate the email confirmation token
         //   var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            // Build the confirmation callback URL
            //var confirmationLink = Url.Action("ConfirmEmail", "Account",
            //    new { UserId = user.Id, Token = token }, protocol: HttpContext.Request.Scheme);

            // Encode the link to prevent XSS and other injection attacks
            var safeLink = HtmlEncoder.Default.Encode(confirmationLink);

            // Craft a more polished email subject
            var subject = "Welcome to Dot Net Tutorials! Please Confirm Your Email";

            // Create a professional HTML body
            // Customize inline styles, text, and branding as needed
            var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Hi {user.FullName},</p>

            <p>Thank you for creating an account at <strong>Dot Net Tutorials</strong>.
            To start enjoying all of our features, please confirm your email address by clicking the button below:</p>

            <p>
                <a href=""{safeLink}"" 
                   style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                          font-weight:bold;border-radius:5px;display:inline-block;"">
                    Confirm Email
                </a>
            </p>

            <p>If the button doesn’t work for you, copy and paste the following URL into your browser:
                <br />
                <a href=""{safeLink}"" style=""color:#007bff;text-decoration:none;"">{safeLink}</a>
            </p>

            <p>If you did not sign up for this account, please ignore this email.</p>

            <p>Thanks,<br />
            The Dot Net Tutorials Team</p>
        </div>
    ";

            //Send the Confirmation Email to the User Email Id
            await sender.SendEmailAsync(email, subject, messageBody, true);
        }

        public async Task EmailFromContactUs(ContactUsDTO contactus)
        {
            string myEmail = "A7mddarwish7Dev@gmail.com";

            string emailBody = $@"
        <h2>New Contact Us Message</h2>
        <p><strong>Name:</strong>  {contactus.name}</p>
        <p><strong>Email:</strong> {contactus.email}</p>
        <p><strong>Phone:</strong> {contactus.phone}</p>
        <hr>
        <p><strong>Message:</strong></p>
        <p>{contactus.message}</p>
        <br>
        <p>--- This message was sent from the Contact Us form ---</p>
    
            ";

            await sender.SendEmailAsync(myEmail, contactus.subject, emailBody, true);
        }
    }
}
