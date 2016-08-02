using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class LoginController : Controller
    {
        private UsersRepository _userRepository;

        public LoginController()
        {
            _userRepository = new UsersRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginFormValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DbUser user = _userRepository.GetByEmail(model.Email);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
                return View(model);
            }

            string hashedPassword = Security.Hash(model.Password);
            if(hashedPassword != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Email, true);
            return Redirect("~/");           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userRepository != null)
                {
                    _userRepository.Dispose();
                    _userRepository = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}