using Auth.IAuthServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthServices
{
    public class AuthTokenGenerator : IAuthTokenGenerator
    {
        readonly IConfiguration _configuration;
        public AuthTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJWTToken(Dictionary<string, string> parameters)
        {
            List<Claim> claims = GetClaims(parameters);
            string secretKey, issuer, audience;
            int expireMinutes;
            GetValuesFromEnviornmentVariables(out secretKey, out issuer, out audience, out expireMinutes);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes), // Token expiration time
                signingCredentials: creds
            );


            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;

        }

        private void GetValuesFromEnviornmentVariables(out string secretKey, out string issuer, out string audience, out int expireMinutes)
        {
            secretKey = _configuration.GetValue<string>("Jwt:Key")!.ToString();
            issuer = _configuration.GetValue<string>("Jwt:Issuer")!.ToString();
            audience = _configuration.GetValue<string>("Jwt:Audience")!.ToString();
            expireMinutes = _configuration.GetValue<int>("Jwt:ExpireMinutes");
        }

        private static List<Claim> GetClaims(Dictionary<string, string> parameters)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var dict in parameters)
            {
                claims.Add(new Claim(dict.Key, dict.Value));
            }

            return claims;
        }
    }
}
