﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } //AccessToken
        public string RefreshToken { get; set; } 

    }
}
