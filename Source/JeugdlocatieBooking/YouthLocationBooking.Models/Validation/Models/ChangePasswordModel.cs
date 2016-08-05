using System.ComponentModel.DataAnnotations;

namespace YouthLocationBooking.Data.Validation.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}
