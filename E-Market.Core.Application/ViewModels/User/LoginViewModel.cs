using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe escribir su nombre de usuario")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe escribir una contraseña")]
        public string Password { get; set; }
    }
}
