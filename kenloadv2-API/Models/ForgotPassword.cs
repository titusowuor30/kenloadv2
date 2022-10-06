using System;
using System.ComponentModel.DataAnnotations;

namespace KenloadV2API.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required")]

        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public String NewPassword { get; set; }
    }
}