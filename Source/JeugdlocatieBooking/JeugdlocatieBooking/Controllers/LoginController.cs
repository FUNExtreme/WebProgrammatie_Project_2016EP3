using System.Web.Mvc;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginFormValidationModel m)
        {
            if (ModelState.IsValid)
            {
                /*
                Person p = Person.GetByEmail(m.Email);
                string passwordHash = Security.Hash(m.Password);

                if (passwordHash == p.Password)
                {

                }
                */
            }

            return View();
        }
    }
}