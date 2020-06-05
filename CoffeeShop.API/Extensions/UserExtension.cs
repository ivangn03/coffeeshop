using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CoffeeShop.API.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.API.Extensions
{
    public static class UserExtension
    {
        public static void GenerateToken(this User user, string secret, int expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, user.FirstName),
                };
            var roleClaims = user.UserRoles.Select(x=>new Claim(ClaimTypes.Role, x.Role.Name));
            claims.AddRange(roleClaims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        //    user.Password = null;
        }
        
    }
}