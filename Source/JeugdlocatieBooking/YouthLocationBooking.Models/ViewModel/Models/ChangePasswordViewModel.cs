using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Oud Wachtwoord")]
        [Required(ErrorMessage = "Oud wachtwoord is verplicht")]
        [StringLength(100, ErrorMessage = "Een wachtwoord mag maximaal 100 karakters lang zijn")]
        public string OldPassword { get; set; }

        [Display(Name = "Nieuw Wachtwoord")]
        [Required(ErrorMessage = "Nieuw wachtwoord is verplicht")]
        [StringLength(100, ErrorMessage = "Een wachtwoord mag maximaal 100 karakters lang zijn")]
        public string NewPassword { get; set; }
    }
}
