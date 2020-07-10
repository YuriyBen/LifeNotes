using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public class LoginService : ILoginService
    {
        private readonly LifeNotesContext _context;
        public LoginService(LifeNotesContext context)
        {
            _context = context;
        }
        public Users GetUserOrDefault(LoginDTO userClaims)
        {
            if (string.IsNullOrEmpty(userClaims.Email) || string.IsNullOrEmpty(userClaims.Password))
            {
                return null;
            }

            string passwordSalt = _context.Users.Where(x => x.Email == userClaims.Email).Select(x => x.PasswordSalt).FirstOrDefault();

            if (passwordSalt == null)
            {
                return null;
            }
            string passwordHash = userClaims.Password.GenerateHash(passwordSalt);
            Users user = _context.Users
                                        .FirstOrDefault(u => u.Email == userClaims.Email
                                                          && u.PasswordHash == passwordHash);


            return user;
        }


    }
}
