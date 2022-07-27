using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtToken.API.Jwt
{
    public static class JwtTokenExtensions
    {
        public static void AddJwtService(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var jwtTokenConfiguration = sp.GetService<JwtTokenConfiguration>();

            if (jwtTokenConfiguration == null)
                throw new ArgumentNullException(nameof(jwtTokenConfiguration));

            AddJwtToken(services, jwtTokenConfiguration);
        }
        public static void AddJwtService(this IServiceCollection services, JwtTokenConfiguration jwtTokenConfiguration)
        {
            if (jwtTokenConfiguration == null)
                throw new ArgumentNullException(nameof(jwtTokenConfiguration));

            AddJwtToken(services, jwtTokenConfiguration);
        }
        public static void AddJwtService(this IServiceCollection services, Action<JwtTokenConfiguration> options)
        {
            JwtTokenConfiguration jwtTokenConfiguration = new JwtTokenConfiguration();
            options(jwtTokenConfiguration);

            AddJwtToken(services, jwtTokenConfiguration);
        }
        private static void AddJwtToken(IServiceCollection services, JwtTokenConfiguration jwtTokenConfiguration)
        {
            services.AddScoped<IJwtService, JwtService>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfiguration.SecretKey));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    //jwtBearerOptions.SaveToken = true; //suitable 
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtTokenConfiguration.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtTokenConfiguration.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateLifetime = true
                    };
                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnForbidden = context => { return Task.CompletedTask; },
                        OnAuthenticationFailed = context => { return Task.CompletedTask; },
                        OnTokenValidated = context => { return Task.CompletedTask; }
                    };
                });
        }

        public static void UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
