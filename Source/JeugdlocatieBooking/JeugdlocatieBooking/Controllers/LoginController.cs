using System.Web.Mvc;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginFormValidationModel m)
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