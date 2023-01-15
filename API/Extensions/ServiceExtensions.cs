using API.Helpers;
using AutoMapper;
using Data.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.LoggerService;


namespace API.Extensions
{
    public static class ServiceExtensions
    {
        private static readonly string _defaultCorsPolicyName = "localhost";
        public static void RegisterService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //DB Context Entity framework
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                 configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Data")));

            //helpers
            serviceCollection.AddScoped<ICommonHelper, CommonHelper>();
            serviceCollection.AddScoped<IPaymentHelper, PaymentHelper>();
            serviceCollection.AddScoped<IEmailHelper, EmailHelper>();
            serviceCollection.AddScoped<IAPIHelper, APIHelper>();
            //serviceCollection.AddScoped<IAramexRateHelper, AramexRateHelper>();
            //serviceCollection.AddScoped<IAramexShippingHelper, AramexShippingHelper>();
            //serviceCollection.AddScoped<IMyFatoorahApiHelper, MyFatoorahApiHelper>();
            serviceCollection.AddScoped<IMasterCardHelper, MasterCardHelper>();
            serviceCollection.AddScoped<ITabbyHelper, TabbyHelper>();
            serviceCollection.AddScoped<IPushNotification, PushNotification>();

            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            serviceCollection.AddScoped<AppSettingsModel>();
        }
        public static void AddAuthentication(this IServiceCollection services, AppSettingsModel appSettings)
        {
            var key = Encoding.UTF8.GetBytes(appSettings.APIKey);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(token =>
           {
               token.RequireHttpsMetadata = false;
               token.SaveToken = true;
               token.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidIssuer = appSettings.Issuer.ToString(),
                   ValidateIssuer = true,
                   ValidAudience = appSettings.Issuer.ToString(),
                   ValidateAudience = true,
                   NameClaimType = JwtRegisteredClaimNames.Sub, //BKD new addition for role based auto authenticate
                   RoleClaimType = ClaimTypes.Role,//BKD new addition for role based auto authenticate
                   ValidateLifetime = true,
                   RequireExpirationTime = true,
                   ClockSkew = TimeSpan.Zero
               };
               token.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                       {
                           context.Response.Headers.Add("Token-Expired", "true");
                       }
                       return Task.CompletedTask;
                   }
               };
           });
        }
        public static void AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }
        public static void AddSwaggerService(this IServiceCollection services)
        {
            //more configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Adhari API v1", Version = "v1" });
                c.OperationFilter<CustomHeaderSwaggerAttribute>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
        public static void AddCors(this IServiceCollection services, AppSettingsModel appSettings)
        {
            string CorsAllowedUrls = appSettings.CorsAllowedUrls;

            // Configure CORS for  UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            CorsAllowedUrls
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }
        public static void UseCors(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            app.UseCors(_defaultCorsPolicyName);
        }
        public static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public class CustomHeaderSwaggerAttribute : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "key",
                    In = ParameterLocation.Header,
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                    },
                    Example = new OpenApiString("d322aca1-8e9a-4b1c-ae14-15250840cd94")
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "lang",
                    In = ParameterLocation.Header,
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                    },
                    Example = new OpenApiString("EN")
                });
            }
        }
    }
}
