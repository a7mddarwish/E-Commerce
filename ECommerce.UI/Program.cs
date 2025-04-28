using ECommerce.Core;
using ECommerce.Core.Domain.IdentityEntities;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ECommerce.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
             {
                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.Http,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "Fill in the JWT token",
                 });
                 options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new List<String>()
                    }
                 });
             });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()   
                          .AllowAnyMethod()   
                          .AllowAnyHeader();  
                });
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.InfrastructureService(builder.Configuration);
            builder.Services.configCoreService();

            builder.Services.AddIdentity<AppUser, AppRoles>()
                               .AddEntityFrameworkStores<AppDbContext>()
                               .AddUserStore<UserStore<AppUser, AppRoles, AppDbContext, Guid>>()
                               .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
              
                    options.Password.RequiredLength = 8;

                    options.Password.RequiredUniqueChars = 2;

                    options.Password.RequireLowercase = true;

                    options.Password.RequireUppercase = true;

                    options.Password.RequireDigit = true;

                    options.Password.RequireNonAlphanumeric = true;

                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
          
                

            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,

                    ValidIssuer = builder.Configuration["securitymodule:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["securitymodule:SecretKey"]!)),

                };



            });

            builder.Services.AddAuthorization(options =>
            {
             options.AddPolicy("Mans" , policy => policy.RequireClaim("Address", "mans"));
               
            });

            var app = builder.Build();

         //   if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
