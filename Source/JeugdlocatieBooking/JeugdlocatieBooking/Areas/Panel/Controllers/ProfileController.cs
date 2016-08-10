using System.Web.Mvc;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Mappings;
using YouthLocationBooking.Data.Database.Repositories;
using YouthLocationBooking.Data.ViewModel.Models;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticateAttribute]
    public class ProfileController : Controller
    {
        #region Variables
        private UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ProfileController()
        {
            _unitOfWork = new UnitOfWork();
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

            var usersRepository = _unitOfWork.UsersRepository;

            // Verify that this new email address is still available
            if (User.Identity.Name.ToLower() != model.Email.ToLower())
            {
                DbUser newEmailUser = usersRepository.GetByEmail(model.Email);
                if(newEmailUser != null)
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
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usersRepository = _unitOfWork.UsersRepository;

            var user = usersRepository.GetByEmail(User.Identity.Name);
            if (user.Password != Security.Hash(model.OldPassword))
            {
                ModelState.AddModelError(string.Empty, "Old Password is incorrect!");
                return View(model);
            }

            user.Password = Security.Hash(model.NewPassword);
            usersRepository.Update(user);

            // TODO add success message
            return View(model);
        }
        #endregion

        #region Dispose
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