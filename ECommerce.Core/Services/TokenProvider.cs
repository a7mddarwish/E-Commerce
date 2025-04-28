using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Core.DTOs;
using ECommerce.Core.ServicesConstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Core.Services
{
    public class TokenProvider : ITokenProvider
    {
        
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration config)
        {
            _configuration = config;
        }

        public siginedinwithtokenDTO GenerateJWTToken(AppUser actuser, IList<string> Roles)
        {

            // Generate JWT Token
            var secritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["securitymodule:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // this will be stored in payload
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Add here role like admin 
                    new Claim("userEmail" , actuser.Email),
                    new Claim("UserID" , actuser.Id.ToString()),
                    new Claim("Address" , actuser.Address),
                    new Claim(ClaimTypes.Role , String.Join(" | ",Roles)),
                    new Claim("How are you?" , Guid.NewGuid().ToString())
                }),
                Issuer = _configuration["securitymodule:Issuer"],
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["securitymodule:LifeTimeInHours"]!)),

                SigningCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256),


            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new siginedinwithtokenDTO { Token = tokenHandler.WriteToken(token), Email = actuser.Email, FullName = actuser.FullName };

        }


    }
        
    }

