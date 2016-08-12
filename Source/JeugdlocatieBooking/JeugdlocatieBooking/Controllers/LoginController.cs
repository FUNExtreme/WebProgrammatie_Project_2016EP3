using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.ViewModel.Models;
using YouthLocationBooking.Web.Code;

namespace YouthLocationBooking.Web.Controllers
{
    public class LoginController : UnitOfWorkControllerBase
    {
        #region Constructor
        public LoginController()
            : base()
        {
        }
        #endregion

        #region Index
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
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            DbUser user = null;
            try
            {
                var userRepository = _unitOfWork.UsersRepository;
                user = userRepository.GetByEmail(model.Email);
                if (user == null)
                {
                    TempData["AlertType"] = "danger";
                    TempData["AlertMessage"] = "Ongeldige aanmeld gegevens.";
                    return View(model);
                }
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het aanmelden!";
                return View(model);
            }

            string hashedPassword = Security.Hash(model.Password);
            if (hashedPassword != user.Password)
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Ongeldige aanmeld gegevens.";
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Email, true);

            string returnUrl = (string)TempData["LoginReturnUrl"];
            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(returnUrl);
        }
        #endregion
    }
}