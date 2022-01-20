using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Northwind.Entity.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Bll
{
    public class TokenManager
    {
        IConfiguration configuration;

        public TokenManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateAccessToken(DtoLoginUser user)
        {
            //claims 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserCode),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            //claims roller
            var claimsRoleList = new List<Claim>  // kimliğe eklenmedi ???
            {
                new Claim("role","Admin")
            };
            //security key, byte a çeviriyoruz

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));

            //şifrelenmiş kimlik oluştur

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //token config
            var token = new JwtSecurityToken
                (
                    issuer: configuration["Token:Issuer"], //dağıtıcı url
                    audience: configuration["Token:Audience"], // karşılayan url
                    expires : DateTime.Now.AddMinutes(5),
                    notBefore: DateTime.Now, //devreye girme süresi
                    signingCredentials : cred,  // kimlik
                    claims:claimsIdentity.Claims 
                );
            var tokenHandler = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

            return tokenHandler.token;

        }
    }
}
