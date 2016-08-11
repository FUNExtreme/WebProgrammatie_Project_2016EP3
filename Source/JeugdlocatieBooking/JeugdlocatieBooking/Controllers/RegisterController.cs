using System.Web.Mvc;

namespace YouthLocationBooking.Web.Controllers
{
    public class RegisterController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            return Redirect("/WebForms/Register.aspx");
        }
        #endregion
    }
}