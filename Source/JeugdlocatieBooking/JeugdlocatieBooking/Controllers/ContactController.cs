using System.Web.Mvc;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View();
        }
    }
}