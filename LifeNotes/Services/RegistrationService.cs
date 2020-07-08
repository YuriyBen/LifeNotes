using LifeNotes.Entities;
using LifeNotes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly LifeNotesContext _context;
        public RegistrationService(LifeNotesContext context)
        {
            _context = context;
        }
        public async Task SignOutUserAsync(Users userToCreate, string plainPassword)
        {
            string passwordSalt = PasswordHashing.CreateSalt();
            userToCreate.PasswordSalt = passwordSalt;
            userToCreate.PasswordHash = PasswordHashing.GenerateHash(plainPassword, passwordSalt);

            await _context.Users.AddAsync(userToCreate);
        }
    }
}
