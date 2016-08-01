using System.Web.Mvc;

namespace YouthLocationBooking.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/WebForms/Register.aspx");
        }
    }
}