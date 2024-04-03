using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Account
{
    public class UpdateUserDTO
    {
        
            [StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be at least {2}, and maximum {1} character")]
            public string? FirstName { get; set; }

            [StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2}, and maximum {1} character")]
            public string? LastName { get; set; }
            
            public string? UserName { get; set; }

            public bool? IsAdmin { get; set; }

            public IFormFile? PersonalImg { get; set; }

            public IFormFile? NImg { get; set; }

            [StringLength(14, MinimumLength = 14)]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
            public string? NId { get; set; }

            [StringLength(11, MinimumLength = 11)]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
            public string? PhoneNumber { get; set; }
        }
    }

