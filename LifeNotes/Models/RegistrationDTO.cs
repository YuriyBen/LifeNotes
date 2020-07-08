using LifeNotes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class RegistrationDTO
    {
        //public long Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage ="Email is incorrect..")]
        public string Email { get; set; }
        public byte[] ProfileImage { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        //[Required]
        //[Compare("Password")]
        //public string PasswordConfirmation { get; set; }
    }
}
