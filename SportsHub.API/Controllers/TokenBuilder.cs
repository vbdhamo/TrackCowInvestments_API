using SportsHub.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SportsHub.API.Controllers
{
    public static class TokenBuilder
    {
        internal static TokenValidationParameters TokenValidationParams;

        #region JWT Token
        public static void ConfigureJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            byte[] SymmetricKeyBytes = GetBinaryKey(appSettings);
            TokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = appSettings.JWTIssuer,
                ValidateLifetime = true,
                ValidAudience = appSettings.JWTAudiance,
                ValidateAudience = true,
                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(SymmetricKeyBytes),
                ClockSkew = TimeSpan.FromMinutes(0)
            };
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = TokenValidationParams;
            });
        }

        private static byte[] GetBinaryKey(AppSettings appSettings)
        {
            byte[] SymmetricKeyBytes;
            //if (appSettings.IsAWSSecretEnabled)
            //{
            //    AppSecrets appSecret = AWSSecretsHelper.GetAppSecrets(appSettings.AWSAppSecretsName);
            //    SymmetricKeyBytes = Encoding.ASCII.GetBytes(appSecret.JWTSecret);
            //}
            //else
            //{
            SymmetricKeyBytes = Encoding.ASCII.GetBytes(appSettings.JWTSecKey);
            //}

            return SymmetricKeyBytes;
        }

        public static string CreateJsonWebToken(string username, string email, AppSettings appSettings, Guid applicationId, DateTime expires, string deviceId = null, bool isReAuthToken = false)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(ClaimTypes.Email, email));

            byte[] SymmetricKeyBytes = GetBinaryKey(appSettings);

            //if (roles != null)
            //{
            //    foreach (var role in roles)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, role));
            //        claims.Add(new Claim(ClaimTypes.Role, role));

            //    }
            //}
            var signinKey = new SymmetricSecurityKey(SymmetricKeyBytes);
            var identity = new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
            var jwt = new SecurityTokenDescriptor
            {
                Issuer = appSettings.JWTIssuer,
                Audience = appSettings.JWTAudiance,
                Subject = identity,
                NotBefore = DateTime.UtcNow,
                Expires = expires,
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };
            var securtiyTokenhandler = new JwtSecurityTokenHandler();
            var securityToken = securtiyTokenhandler.CreateToken(jwt);
            return securtiyTokenhandler.WriteToken(securityToken);
        }

        #endregion
    }
}
