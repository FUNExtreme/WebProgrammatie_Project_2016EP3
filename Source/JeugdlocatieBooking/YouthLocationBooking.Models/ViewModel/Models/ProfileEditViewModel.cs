using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ProfileEditViewModel
    {
        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Voornaam is verplicht")]
        [StringLength(100, ErrorMessage = "Voornaam mag maximaal 100 karakters lang zijn")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Achternaam is verplicht")]
        [StringLength(100, ErrorMessage = "Achternaam mag maximaal 100 karakters lang zijn")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig email formaat")]
        [StringLength(255, ErrorMessage = "Email mag maximaal 255 karakters lang zijn")]
        public string Email { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [Phone(ErrorMessage = "Ongeldig telefoonnummer formaat")]
        [StringLength(30, ErrorMessage = "Telefoonnummer mag maximaal 30 karakters lang zijn")]       
        public string PhoneNumber { get; set; }
    }
}
