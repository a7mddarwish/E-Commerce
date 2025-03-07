using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.DTOs;
using ECommerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly IConfiguration config;
        private readonly EmailSender emailsender;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> usermanager, IConfiguration config, EmailSender emailsender , IMapper mapper)
        {
            this.usermanager = usermanager;
            this.config = config;
            this.emailsender = emailsender;
            this.mapper = mapper;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registration(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { modelState = ModelState });

            AppUser user = new AppUser
            {
                // will assume user name as an email to login with it
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.FullName,
                Address = registerDTO.Address
            };

            IdentityResult result = await usermanager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("Regester", error.Description);
                return BadRequest(new { modelState = ModelState });

            }

           await usermanager.AddToRoleAsync(user, "Customer");

            string token = await usermanager.GenerateEmailConfirmationTokenAsync(user);

            // Build the confirmation callback URL
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { UserEmail = user.Email, Token = token }, protocol: HttpContext.Request.Scheme);

            await emailsender.SendConfirmationEmail(user.Email, user, token, confirmationLink);


            return Ok(new {message = "Please Check email now"});
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO logininfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Enter valied Data" });

            AppUser actuser = await usermanager.FindByEmailAsync(logininfo.Email);

            if (actuser == null || !await usermanager.CheckPasswordAsync(actuser, logininfo.Password))
                return BadRequest(new { message = $"Email or password is incorrect." });

            if (!await usermanager.IsEmailConfirmedAsync(actuser))
            {
                return BadRequest(new { message = "Email not confirmed" });
            }



            return Ok(new { token = await GenerateToken(actuser) });

        }

        private async Task<string> GenerateToken(AppUser actuser)
        {

            // Generate JWT Token
            var secritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config["securitymodule:SecretKey"]));

            var Roles = await usermanager.GetRolesAsync(actuser);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // this will be stored in payload
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Add here role like admin 
                    //Addded in Claims catch it using Claimsprincipls
                    new Claim("userEmail" , actuser.Email),
                    new Claim("UserID" , actuser.Id.ToString()),
                    new Claim("Address" , actuser.Address),
                    new Claim(ClaimTypes.Role , Roles.First()),
                    new Claim("How are you?" , Guid.NewGuid().ToString())
                }),
                Issuer = config["securitymodule:Issuer"],
                Expires = DateTime.UtcNow.AddHours(int.Parse(config["securitymodule:LifeTimeInHours"]!)),

                SigningCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256),


            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string userEmail, string Token)
        {
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(Token))
            {
                // Provide a descriptive error message for the view
                return BadRequest(new { message = "Email not confirmed" });

            }
            //Find the User by Email
            var user = await usermanager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                // Provide a descriptive error for a missing user scenario
                return BadRequest(new { message = "We could not find a user associated with the given link." });
            }
            // Attempt to confirm the email
            var result = await usermanager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Thank you for confirming your email address. Your account is now verified!" });

            }
            // If confirmation fails
            return BadRequest(new { message = "We were unable to confirm your email address. Please try again or request a new link." });

        }


        [HttpGet("ReConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReConfirmEmail(string Email)
        {

            var user = await usermanager.FindByEmailAsync(Email);
            if (user == null || await usermanager.IsEmailConfirmedAsync(user))

                return BadRequest(new { message = "User already Confirmed !" });

            string token = await usermanager.GenerateEmailConfirmationTokenAsync(user);

            // Build the confirmation callback URL
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { UserEmail = user.Email, Token = token }, protocol: HttpContext.Request.Scheme);

            await emailsender.SendConfirmationEmail(user.Email, user, token, confirmationLink);


            return Ok(new { message = "Check email again" });
        }


        [HttpGet("UserInfo")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Userinfo()
        {
            var userId = User.FindFirstValue("UserID");

            if (string.IsNullOrEmpty(userId))            
                return Unauthorized(new { error = "UserID claim not found" });
            
            var user = await usermanager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { error = "User not found" });

            return Ok(mapper.Map<userDTO>(user));
        }

    }
}