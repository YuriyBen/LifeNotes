using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using LifeNotes.Services;
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
        private readonly ILoginService _loginRepository;

        public LoginController(LifeNotesContext context, IOptions<JWTSettings> jwtSettings, IMapper mapper,ILoginService loginRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _loginRepository=loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        }
        [AllowAnonymous]
        [HttpPost()]
        [Route("api/login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO userClaims)
        {
            Users user = _loginRepository.GetUserOrDefault(userClaims);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect..." });
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = GenerateJWT.CreateJWT(userDTO.Id,_jwtSettings.SecretKey);

            userDTO.Token = tokenHandler.WriteToken(token);
            user.RegistrationToken = tokenHandler.WriteToken(token);

            await _context.SaveChangesAsync();

            return userDTO;
        }
    }
}
