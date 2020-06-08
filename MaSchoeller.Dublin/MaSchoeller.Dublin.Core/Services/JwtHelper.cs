using MaSchoeller.Dublin.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaSchoeller.Dublin.Core.Abstracts;
using Microsoft.Extensions.Logging;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class JwtHelper : ISecurityHelper
    {
        private readonly ServerConfiguration _configuration;
        private readonly ILogger<JwtHelper>? _logger;
        private readonly JwtSecurityTokenHandler _handler;
        public JwtHelper(ServerConfiguration configuration, ILogger<JwtHelper>? logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger;
            _handler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.TokenSecret);
            var listClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
            };

            var roles = Enumerable.Empty<string>();
            foreach (var role in roles)
            {
                listClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listClaims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _handler.CreateToken(tokenDescriptor);
            return _handler.WriteToken(token);
        }


        public bool ValidateToken(string token)
        {
            try
            {
                var claims = _handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.TokenSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                }, out var result);
                return true;
            }
            catch (Exception e)
            {
                //Todo: maybe log the message?
                return false;
            }

        }
    }
}
