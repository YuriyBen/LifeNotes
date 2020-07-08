using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public class LoginController : ControllerBase
    {
        private readonly LifeNotesContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public LoginController(LifeNotesContext context, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [AllowAnonymous]
        [HttpPost()]
        [Route("api/login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO userClaims)
        {
            if (string.IsNullOrEmpty(userClaims.Email) || string.IsNullOrEmpty(userClaims.Password))
            {
                return BadRequest(new { message = "Email or password is empty..." });
            }
            
            string passwordSalt = _context.Users.Where(x => x.Email == userClaims.Email).Select(x => x.PasswordSalt).FirstOrDefault();

            if (passwordSalt == null)
            {
                return BadRequest(new { message = "It seems that email doesn't exist or is incorrect..." });
            }
            string passwordHash = userClaims.Password.GenerateHash(passwordSalt);

            Users user = await _context.Users
                                        .FirstOrDefaultAsync(u => u.Email == userClaims.Email
                                                          && u.PasswordHash == passwordHash);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect..." });
            }
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDTO.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDTO.IdToken = tokenHandler.WriteToken(token);

            user.RegistrationToken = tokenHandler.WriteToken(token);
            await _context.SaveChangesAsync();

            return userDTO;
        }
    }
}
