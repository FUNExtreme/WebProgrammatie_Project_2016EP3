using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.ViewModel.Models;
using YouthLocationBooking.Web.Code;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticate]
    public class ProfileController : UnitOfWorkControllerBase
    {
        #region Constructor
        public ProfileController()
            : base()
        {
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Edit", "Profile");
        }
        #endregion

        #region Edit
        public ActionResult Edit()
        {
            var usersRepository = _unitOfWork.UsersRepository;
            DbUser user = usersRepository.GetByEmail(User.Identity.Name);

            ProfileEditViewModel validationModel = user.ToProfileEditValidationModel();
            return View(validationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var usersRepository = _unitOfWork.UsersRepository;

                // Verify that this new email address is still available
                if (User.Identity.Name.ToLower() != model.Email.ToLower())
                {
                    DbUser newEmailUser = usersRepository.GetByEmail(model.Email);
                    if (newEmailUser != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email address is already in use, please use a different one");
                        ModelState.AddModelError("Email", "Already in use");
                        return View(model);
                    }
                }

                DbUser user = usersRepository.GetByEmail(User.Identity.Name);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                usersRepository.Update(user);

                FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(model.Email, true);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "De wijzigingen zijn opgeslagen.";
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van de wijzigingen!";
            }

            return View(model);
        }
        #endregion

        #region ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var usersRepository = _unitOfWork.UsersRepository;
                var user = usersRepository.GetByEmail(User.Identity.Name);
                if (user.Password != Security.Hash(model.OldPassword))
                {
                    ModelState.AddModelError(string.Empty, "Old Password is incorrect!");
                    return View(model);
                }

                user.Password = Security.Hash(model.NewPassword);
                usersRepository.Update(user);

                TempData["AlertType"] = "success";
                TempData["AlertMessage"] = "Het wachtwoord is opgeslagen. U kan vanaf nu aanmelden met uw nieuw wachtwoord.";
            }
            catch
            {
                TempData["AlertType"] = "danger";
                TempData["AlertMessage"] = "Er is iets fout gelopen tijdens het verwerken van het nieuwe wachtwoord!";
            }

            return View(model);
        }
        #endregion
    }
}