using System.Web.Mvc;

namespace YouthLocationBooking.Web.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/WebForms/Register.aspx");
        }
    }
}