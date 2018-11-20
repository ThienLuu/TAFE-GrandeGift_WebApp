using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;

namespace Luu_DiplomaProject.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Username cannot be blank.")]
        [MaxLength(256, ErrorMessage = "Username cannot exceed 256 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name cannot be blank.")]
        [MaxLength(256, ErrorMessage = "First name cannot exceed 256 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be blank.")]
        [MaxLength(256, ErrorMessage = "Last name cannot exceed 256 characters.")]
        public string LastName { get; set; }

        //VALIDATION FOR DOB 18+
        [Required (ErrorMessage = "Date Of Birth is Required.")]
        public DateTime DOB { get; set; }

        [Required (ErrorMessage = "Email cannot be blank.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Please confirm Email Address.")]
        [Compare(nameof(Email), ErrorMessage = "Email Address does not match.")]
        public string ConfirmEmail { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //public string Phone { get; set; }

        [Required (ErrorMessage = "Password cannot be blank."), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm Password."), DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
