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
using ECommerce.Core.ServicesConstracts.Email;
using ECommerce.Core.ServicesConstracts;

namespace ECommerce.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly IConfiguration config;
        private readonly IEmailSender emailsender;
        private readonly IMapper mapper;
        private readonly ITokenProvider _tokenProvider;

        public AccountController(UserManager<AppUser> usermanager, IConfiguration config, IEmailSender emailsender, 
            IMapper mapper , ITokenProvider tokenProvider)
        {
            this.usermanager = usermanager;
            this.config = config;
            this.emailsender = emailsender;
            this.mapper = mapper;
            this._tokenProvider = tokenProvider;
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

            await emailsender.SendVarificationEmail(user.Email, user, token, confirmationLink);


            return Ok(new { message = "Please Check email now" });
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



            return Ok(_tokenProvider.GenerateJWTToken(actuser , await usermanager.GetRolesAsync(actuser)));

        }


        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string userEmail, string Token)
        {
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(Token))
            {
                return BadRequest(new { message = "Email not confirmed" });

            }
            var user = await usermanager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest(new { message = "We could not find a user associated with the given link." });
            }
            var result = await usermanager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Thank you for confirming your email address. Your account is now verified!" });

            }
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

            await emailsender.SendVarificationEmail(user.Email, user, token, confirmationLink);


            return Ok(new { message = "Check email again" });
        }


        [HttpGet("UserInfo")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Userinfo()
        {
            string? userId = User.FindFirst(c => c.Type == "UserID")?.Value;


            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { error = "UserID claim not found" });

            var user = await usermanager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { error = "User not found" });

            return Ok(mapper.Map<userDTO>(user));
        }


        [Authorize]
        [HttpPut("EditUerProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditUserProfileinfo(ChangeUserInfoDTO userinfodto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Enter valied info" });
            string? IDClaim = User.FindFirst(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(IDClaim))
                return Unauthorized(new { message = "User Not Found" });

            AppUser user = await usermanager.FindByIdAsync(IDClaim);

            if (!await usermanager.CheckPasswordAsync(user, userinfodto.passwordToConfirm))
                return BadRequest(new { message = "Password was wrong , Please check again or reset the password" });


            if (user == null)
                return StatusCode(500, new { message = "Internal server error." });


            user.FullName = userinfodto.FullName;
            user.Address = userinfodto.Address;

            await usermanager.UpdateAsync(user);

            return Ok(mapper.Map<userDTO>(user));

        }


        [Authorize]
        [HttpPatch("ChangeEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeEmail(string email, string passwordToConfirm)
        {
            string userId = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User Not found" });

            AppUser user = await usermanager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { message = "User dosenot exist" });


            if (!await usermanager.CheckPasswordAsync(user, passwordToConfirm))
                return BadRequest(new { message = "Password was wrong , Please check again or reset the password" });

            user.Email = email;
            user.NormalizedEmail = email.ToUpper();

            user.UserName = email;
            user.NormalizedUserName = email.ToUpper();



            await usermanager.UpdateNormalizedEmailAsync(user);
            await usermanager.UpdateNormalizedUserNameAsync(user);


            return Ok(mapper.Map<userDTO>(user));
        }


        [Authorize]
        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(string oldpass, string NewPass, string confirmednewpass)
        {
            if (string.IsNullOrEmpty(oldpass) || string.IsNullOrEmpty(NewPass) || string.IsNullOrEmpty(confirmednewpass))
                return BadRequest(new { message = "All filds is required" });

            if (NewPass != confirmednewpass)
                return BadRequest(new { message = "Enter same password in the two filds" });

            string userid = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userid))
                return Unauthorized(new { message = "User not found" });

            AppUser user = await usermanager.FindByIdAsync(userid);

            if (user == null)
                return NotFound(new { message = "User dosenot exist!" });

            IdentityResult result = await usermanager.ChangePasswordAsync(user, oldpass, NewPass);

            if (!result.Succeeded)
                return StatusCode(500, new { message = "internal server Error" });


            return Ok("Password changed successfully");

        }

        [HttpGet("ForgetPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { message = "Enter valid emial" });

            AppUser user = await usermanager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { message = "this email does not related to any user , Register now!" });

            string _token = await usermanager.GeneratePasswordResetTokenAsync(user);

            string redirectionurl = $"http://localhost:3000/ForgetPassword?token={_token}";

            await emailsender.SendResetPasswoedEmail(email, user, _token, redirectionurl);

            return Ok(new { message = "Check email" });

        }

        [HttpPut("SetNewPassword", Name = "SetNewPassword")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetNewPassword(ReSetPasswordDTO inputs)
        {
            if (!ModelState.IsValid) { return BadRequest(new { message = "All fields is required , try again!" }); }

            if (!inputs.NewPassword.Equals(inputs.ConfirmNewPassword))
                return BadRequest(new { message = "password and confirm password does not same"} );

            AppUser user = await usermanager.FindByEmailAsync(inputs.Email);
            if (user == null)
                return NotFound(new {message = $"User with Email:{inputs.Email} does not exist. Register Now!"});
          IdentityResult result = await usermanager.ResetPasswordAsync(user , inputs.Token , inputs.ConfirmNewPassword);

            if (result.Succeeded)
                return Ok("Password changed succesfully");

            return StatusCode(500, new { message = "some thing went wrong while change password" });
        }


    }
}