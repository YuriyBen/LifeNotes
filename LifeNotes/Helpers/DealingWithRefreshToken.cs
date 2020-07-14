using LifeNotes.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LifeNotes.Helpers
{
    public  class DealingWithRefreshToken
    {
        public int GetUserIdFromAccessToken(string accessToken,string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;


            var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                                            StringComparison.InvariantCultureIgnoreCase))
            {
                var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                return Convert.ToInt32(userId);
            }
            return 0;
        }

        public bool ValidateRefreshToken(TblRefreshToken refreshTokenUser,Users userFromDbViaAccessToken, string refreshToken)
        {
            if (refreshTokenUser != null && refreshTokenUser.UserId == userFromDbViaAccessToken.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        public TblRefreshToken GenerateRefreshToken(int _expiryTimeSeconds)
        {
            TblRefreshToken refreshToken = new TblRefreshToken();

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.ExpiryDate = DateTime.UtcNow.AddSeconds(_expiryTimeSeconds); //TODO Change the expiry time
            return refreshToken;
        }
    }
}
