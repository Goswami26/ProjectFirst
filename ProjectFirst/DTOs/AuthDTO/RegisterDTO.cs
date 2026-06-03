using System.ComponentModel.DataAnnotations;

namespace ProjectFirst.DTOs.AuthDTO
{
    public class RegisterDTO
    {
        public string name { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        //[RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|in)$",
        //ErrorMessage = "Email must end with .com or .in")]
        public string email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Mobile number is required")]
        //[RegularExpression(@"^[0-9]{10}$",
        //ErrorMessage = "Mobile number must be exactly 10 digits")]
        public string MobileNo { get; set; } = string.Empty;
    }
}
