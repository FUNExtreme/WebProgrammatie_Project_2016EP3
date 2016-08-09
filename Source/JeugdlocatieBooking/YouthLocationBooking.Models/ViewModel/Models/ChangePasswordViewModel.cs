using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}
