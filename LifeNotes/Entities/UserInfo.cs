using System;
using System.Collections.Generic;

namespace LifeNotes.Entities
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Note = new HashSet<Note>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] ImageData { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public byte UnseccessfulAttemptsCount { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string RegistrationToken { get; set; }

        public virtual ICollection<Note> Note { get; set; }
    }
}
