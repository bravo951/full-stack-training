using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Models
{
    public class UserRegisterRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "The password")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
