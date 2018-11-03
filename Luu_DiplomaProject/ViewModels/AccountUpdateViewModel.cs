using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class AccountUpdateViewModel
    {
        //public string UserId { get; set; }
        //[Required, MaxLength(256)]
        //public string Username { get; set; }

        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
        //[Required, DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //[Required, DataType(DataType.EmailAddress), Compare(nameof(Email))]
        //public string ConfirmEmail { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //public string Phone { get; set; }

        //[Required, DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required, DataType(DataType.Password)]
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }
    }
}
