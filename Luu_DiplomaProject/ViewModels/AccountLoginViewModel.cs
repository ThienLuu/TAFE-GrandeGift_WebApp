using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;

namespace Luu_DiplomaProject.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        //[Required, DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
