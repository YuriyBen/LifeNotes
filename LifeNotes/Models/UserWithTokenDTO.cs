using LifeNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{

    public class UserWithTokenDTO : UserInfo
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        
    }
}
