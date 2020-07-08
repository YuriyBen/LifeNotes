using System;
using System.Collections.Generic;

namespace LifeNotes.Entities
{
    public partial class Users
    {
        public Users()
        {
            Notes = new HashSet<Notes>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] ProfileImage { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string RegistrationToken { get; set; }

        public virtual ICollection<Notes> Notes { get; set; }
    }
}
