using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LifeNotes.Controllers
{
    [ApiController]
    public class AuthorizationController: ControllerBase
    {
        private readonly LifeNotesContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private const int _expiryTimeSeconds = 3600;

        public AuthorizationController(LifeNotesContext context, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpPost()]
        [Route("api/refreshToken")] 
        public ActionResult<RefreshRequest> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            DealingWithRefreshToken dealingWithRefreshToken = new DealingWithRefreshToken();

            string refreshToken = Request.Headers["Authorization"].ToString().Split(" ")[1];
            refreshRequest.RefreshToken = refreshToken;
            int userId = dealingWithRefreshToken.GetUserIdFromAccessToken(refreshRequest.AccessToken, _jwtSettings.SecretKey);

            Users userFromDbViaAccessToken = _context.Users.FirstOrDefault(u => u.Id==userId);

            TblRefreshToken refreshTokenUser = _context.TblRefreshToken
                                                       .Where(rt => rt.RefreshToken == refreshToken)
                                                       .OrderByDescending(x => x.ExpiryDate)
                                                       .FirstOrDefault();

            if (userFromDbViaAccessToken != null && dealingWithRefreshToken.ValidateRefreshToken(refreshTokenUser, userFromDbViaAccessToken, refreshToken))
            {
                UserDTO userWithTokens = _mapper.Map<UserDTO>(userFromDbViaAccessToken);
                userWithTokens.Token = GenerateJWT.CreateJWT(userFromDbViaAccessToken.Id, _jwtSettings.SecretKey, DateTime.UtcNow.AddSeconds(_expiryTimeSeconds));
                RefreshRequest userTokens = new RefreshRequest();

                var userFromDb = _context.TblRefreshToken.FirstOrDefault(x => x.User.RegistrationToken == refreshRequest.AccessToken);
                userFromDb.User.RegistrationToken = userWithTokens.Token;
                string newRefreshToken = Guid.NewGuid().ToString();
                userFromDb.RefreshToken = newRefreshToken;

                _context.SaveChanges();
                userTokens.AccessToken = userWithTokens.Token;
                userTokens.RefreshToken = newRefreshToken;
                return Ok(userTokens);
            }
            return null;


        }

       
    }
}

