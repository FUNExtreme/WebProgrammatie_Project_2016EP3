using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Uw Naam")]
        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 karakters lang zijn")]
        public string Name { get; set; }

        [Display(Name = "Uw Email")]
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress]
        [StringLength(255, ErrorMessage = "Email mag maximaal 255 karakters lang zijn")]
        public string Email { get; set; }

        [Display(Name = "Onderwerp")]
        [Required(ErrorMessage = "Onderwerp is verplicht")]
        [StringLength(150, ErrorMessage = "Onderwerp mag maximaal 150 karakters lang zijn")]
        public string Subject { get; set; }

        [Display(Name = "Bericht")]
        [Required(ErrorMessage = "Bericht is verplicht")]
        [StringLength(500, ErrorMessage = "Bericht mag maximaal 500 karakters lang zijn")]
        public string Message { get; set; }
    }
}
