using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Web.Controllers
{
    public class LoginController : Controller
    {
        #region Variables
        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LoginController()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        public ActionResult Index(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (!string.IsNullOrWhiteSpace(returnUrl))
                TempData["LoginReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            var userRepository = _unitOfWork.UsersRepository;

            if (!ModelState.IsValid)
                return View(model);

            DbUser user = userRepository.GetByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
                return View(model);
            }

            string hashedPassword = Security.Hash(model.Password);
            if (hashedPassword != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Email, true);

            string returnUrl = (string)TempData["LoginReturnUrl"];
            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(returnUrl);
        }

        #region IDispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}