using MaSchoeller.Dublin.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Communications;
using System.Linq;
using MaSchoeller.Dublin.Core.Models;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class JwtHelper : ISecurityHelper
    {
        public const string AdminIdentifier = "admin";
        public const string NameIdentifier = "username";
        public const string FullnameIdentifier = "fullname";
        public const string RoleIdentifier = "role";

        private readonly ServerConfiguration _configuration;
        private readonly IUserRepository _repository;
        private readonly ILogger<JwtHelper>? _logger;
        private readonly JwtSecurityTokenHandler _handler;
        public JwtHelper(ServerConfiguration configuration,
                         IUserRepository repository,
                         ILogger<JwtHelper>? logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
            _handler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.TokenSecret);
            var listClaims = new List<Claim>()
            {
                new Claim(NameIdentifier, user.Username),
                new Claim(FullnameIdentifier, user.FullName),
            };
            if (user.IsAdmin)
                listClaims.Add(new Claim(RoleIdentifier, AdminIdentifier));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listClaims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _handler.CreateToken(tokenDescriptor);
            return _handler.WriteToken(token);
        }

        public UserContext? GetUserContext(string token)
        {
            var jwtToken = _handler.ReadJwtToken(token);
            var userContext = new UserContext();
            if (jwtToken is null)
                return null;

            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type == NameIdentifier)
                    userContext.Username = claim.Value;
                if (claim.Type == RoleIdentifier && claim.Value == AdminIdentifier)
                    userContext.IsAdmin = true;
            }
            return userContext;
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
        public (bool, User?) Login(string username, string password)
        {
            var user = _repository.FindByName(username);
            if (user is null)
                return (false,null);
            return (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash), user);
        }

        public bool UpdatePassword(string username, string oldPassword, string newPassword)
        {
            var user = _repository.FindByName(username);
            if (user is null) return false;
            if (oldPassword == newPassword) return false;

            try
            {
                var newHash = BCrypt.Net.BCrypt.ValidateAndReplacePassword(oldPassword, user.PasswordHash, newPassword);
                if (newHash is null) return false;
                user.PasswordHash = newHash;
                _repository.Update(user);
                return true;
            }
            catch (Exception e)
            {
                //Todo: add logging.
                return false;
            }
        }
    }
}
