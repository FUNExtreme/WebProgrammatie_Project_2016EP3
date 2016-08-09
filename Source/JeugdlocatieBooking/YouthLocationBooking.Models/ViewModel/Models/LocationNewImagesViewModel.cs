using System.Collections.Generic;
using System.Web;

namespace YouthLocationBooking.Data.ViewModel.Models
{
    public class LocationNewImagesViewModel
    {
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }
}
