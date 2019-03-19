using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Challenge_App.Repo.DTO.User
{
    public class UserForRegistrationDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specific password between 4 and 8")]
        public string Password { get; set; }
    }
}
