using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Account
{
    public class CheckEmailDTO
    {
        [Required]
        [RegularExpression("^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z]+\\.[a-zA-Z]{2,})$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required, StringLength(14, MinimumLength = 14)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string NId { get; set; }
    }
}
