using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace UserRegistration.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage ="Please input full name.")]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage ="Special character should not be entered.")]
        public string FullName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please input email.")]
        public string Email { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Please input mobile number.")]
        public string MobileNumber { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Please input address.")]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Special character should not be entered.")]
        public string Address { get; set; }

        [MinLength(6, ErrorMessage = "The user name is at least 6 characters long.")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please input user name.")]
        [Index(nameof(Url), IsUnique = true)]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "User Name contains only letters and numbers.")]
        public string UserName { get; set; }

        [MinLength(8, ErrorMessage = "The password is at least 8 characters long.")]
        [MaxLength(200)]
        [Required(ErrorMessage = "Please input password.")]
        public string Password { get; set; }
       
    }
}