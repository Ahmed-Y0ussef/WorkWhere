using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.Account
{
    public class RegisterDTO
    {
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be at least {2}, and maximum {1} character")]
        public string FirstName { get; set; }
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2}, and maximum {1} character")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z]+\\.[a-zA-Z]{2,})$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long.")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
        [Required]
        public string PersonalImg { get; set; }
        [Required]
        public string NImg {  get; set; }
        [Required, StringLength(14,MinimumLength =14)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string NId { get; set; }
        [Required, StringLength(11,MinimumLength =11)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string PhoneNumber { get; set; }

    }

    public class UploadedFile : IFormFile
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }

        // Implementations for required IFormFile methods
        public void CopyTo(Stream target)
        {
            // Implement logic to copy the uploaded file content to the provided stream (e.g., using file system operations)
            throw new NotImplementedException("CopyTo is not implemented in this example class.");
        }

        public Stream OpenReadStream()
        {
            // Implement logic to open a read stream for the uploaded file content (e.g., using file system operations)
            throw new NotImplementedException("OpenReadStream is not implemented in this example class.");
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        // Implementations for optional IFormFile properties and methods (if needed)
        public string Name { get; set; }
        public string ContentDisposition { get; set; }
        public IHeaderDictionary Headers { get; set; }
    }

}
