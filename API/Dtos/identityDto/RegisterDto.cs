﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos.identityDto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        //   ErrorMessage = "must have 1 uppercase ,1 lowercase, 1 number , 1 char @# & , at least 6 characters ")]

        public string Password { get; set; }
    }
}
