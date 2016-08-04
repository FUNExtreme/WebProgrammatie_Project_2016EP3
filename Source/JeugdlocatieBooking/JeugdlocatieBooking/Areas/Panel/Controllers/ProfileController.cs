using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        #region Variables
        private UsersRepository _usersRepository;
        #endregion

        #region Constructor
        public ProfileController()
        {
            _usersRepository = new UsersRepository();
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
            DbUser user = _usersRepository.GetByEmail(User.Identity.Name);
            ProfileEditValidationModel validationModel = user.ToProfileEditValidationModel();
            return View(validationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEditValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verify that this new email address is still available
            if (User.Identity.Name.ToLower() != model.Email.ToLower())
            {
                DbUser newEmailUser = _usersRepository.GetByEmail(model.Email);
                if(newEmailUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is already in use, please use a different one");
                    ModelState.AddModelError("Email", "Already in use");
                    return View(model);
                }
            }

            DbUser user = _usersRepository.GetByEmail(User.Identity.Name);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            _usersRepository.Update(user);

            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(model.Email, true);

            // TODO add success message
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
        public ActionResult ChangePassword(ChangePasswordValidationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _usersRepository.GetByEmail(User.Identity.Name);
            if (user.Password != Security.Hash(model.OldPassword))
            {
                ModelState.AddModelError(string.Empty, "Old Password is incorrect!");
                return View(model);
            }

            user.Password = Security.Hash(model.NewPassword);
            _usersRepository.Update(user);

            // TODO add success message
            return View(model);
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_usersRepository != null)
                {
                    _usersRepository.Dispose();
                    _usersRepository = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}