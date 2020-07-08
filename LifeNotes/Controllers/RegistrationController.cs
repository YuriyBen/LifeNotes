using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Controllers
{
    [ApiController]
    public class RegistrationController:ControllerBase
    {
        private readonly LifeNotesContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public RegistrationController(LifeNotesContext context, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("api/registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDTO user)
        {
            if (_context.UserInfo.Any(x => x.UserName == user.Username))
            {
                return StatusCode(409, $"User '{user.Username}' is already exists.");
            }
            if (_context.UserInfo.Any(x => x.Email == user.Email))
            {
                return StatusCode(409, $"Email  '{user.Email}' is currently in use.");
            }
            try
            {
                var userToCreate = _mapper.Map<UserInfo>(user);

                string passwordSalt = user.Password.CreateSalt();
                userToCreate.PasswordSalt = passwordSalt;
                userToCreate.PasswordHash = user.Password.GenerateHash(passwordSalt);
                userToCreate.EmailConfirmationToken = Guid.NewGuid().ToString();

                await _context.UserInfo.AddAsync(userToCreate);
                await _context.SaveChangesAsync();
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
