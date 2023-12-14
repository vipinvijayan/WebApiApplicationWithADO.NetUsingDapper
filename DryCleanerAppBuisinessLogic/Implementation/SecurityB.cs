using DryCleanerAppBussinessLogic.Interfaces;
using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Models;
using DryCleanerAppDataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DryCleanerAppBussinessLogic.Implementation
{

    public class SecurityB : ISecurityB
    {
        ISecurityRepository _securityRepository;
        readonly IConfiguration _configuration;
        public SecurityB(ISecurityRepository securityRepository, IConfiguration configuration)
        {
            _securityRepository = securityRepository;
            _configuration = configuration;
        }
        public async Task<string> GenerateJWTToken(string userName, int userId,string userAgent)
        {


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSignInKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Issuer","DryCleaner"),
                new Claim("Admin","true"),
                new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                new Claim("UserId",userId.ToString()),
                new Claim("UserAgent",userAgent)
            };
            var token = new JwtSecurityToken("DryCleaner",
                "DryCleaner",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public RefreshTokenModel GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshTokenModel
            {
                RefreshToken = GetUniqueToken(),
                // token is valid for 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,

            };

            return refreshToken;

        }
        private string GetUniqueToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<string> SaveRefreshToken(UserProfileModel param, string ipAddress)
        {
            RefreshTokenModel jwtRefreshToken = GenerateRefreshToken(ipAddress);
            var refreshToken = new RefreshTokenEntity()
            {
                UserId = param.Id,
                RefreshToken = jwtRefreshToken.RefreshToken,
                Expires = jwtRefreshToken.Expires,
                CreatedByID = param.Id.ToString(),
                IsDeleted = false,
                IsActive = true,
                CompanyId = param.CompanyId,


            };
            string result = await _securityRepository.SaveRefreshToken(refreshToken);

            if (result == GeneralDTO.SuccessMessage)
            {
                return jwtRefreshToken.RefreshToken;
            }
            else
            {
                return GeneralDTO.FailedMessage;
            }

        }

        public string ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTSignInKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidAudience = "DryCleaner",
                    ValidIssuer = "DryCleaner",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;
                var jku = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;
                var userName = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;

                return userName;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> GetActiveStatusOfToken(string token)
        {
            return await _securityRepository.GetActiveStatusOfToken(token);
        }


    }
}
