using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using LifeNotes.Services;
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
        private readonly IRegistrationService _registrationRepository;

        public RegistrationController(LifeNotesContext context, IOptions<JWTSettings> jwtSettings, IMapper mapper, IRegistrationService registrationRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _registrationRepository= registrationRepository ?? throw new ArgumentNullException(nameof(registrationRepository));
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("api/registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDTO user)
        {
            if (_context.Users.Any(x => x.UserName == user.Username))
            {
                return StatusCode(409, $"User '{user.Username}' is already exists.");
            }
            if (_context.Users.Any(x => x.Email == user.Email))
            {
                return StatusCode(409, $"Email  '{user.Email}' is currently in use.");
            }
            try
            {
                var userToCreate = _mapper.Map<Users>(user);

                await _registrationRepository.SignOutUserAsync(userToCreate, user.Password);

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
