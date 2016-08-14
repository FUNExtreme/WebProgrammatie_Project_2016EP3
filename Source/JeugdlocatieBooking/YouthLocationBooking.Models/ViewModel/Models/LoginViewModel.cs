using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig email formaat")]
        [StringLength(255, ErrorMessage = "Email mag maximaal 255 karakters lang zijn")]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Wachtwoord mag maximaal 100 karakters lang zijn")]
        public string Password { get; set; }
    }
}
