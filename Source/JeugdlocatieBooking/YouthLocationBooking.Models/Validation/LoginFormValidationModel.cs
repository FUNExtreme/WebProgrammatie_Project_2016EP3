using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Models.Validation
{
    public class LoginFormValidationModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
