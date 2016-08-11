using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Uw Naam")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Uw Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Onderwerp")]
        [Required]
        public string Subject { get; set; }

        [Display(Name = "Bericht")]
        [Required]
        public string Message { get; set; }
    }
}
