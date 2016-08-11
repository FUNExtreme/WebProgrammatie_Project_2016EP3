using System;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;

namespace YouthLocationBooking.Web.WebForms
{
    public partial class Register : System.Web.Mvc.ViewPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Session["AlertType"] = null;
            Session["AlertMessage"] = null;
        }

		protected void registerSubmit_Click(object sender, EventArgs e)
		{
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    UsersRepository usersRepository = unitOfWork.UsersRepository;
                    if (usersRepository.GetByEmail(_registerEmail.Text) != null)
                    {
                        Session["AlertType"] = "danger";
                        Session["AlertMessage"] = "Er is iets foutgelopen tijdens het registreren";
                        return;
                    }  

                    DbUser user = new DbUser
                    {
                        FirstName = _registerFirstName.Text,
                        LastName = _registerLastName.Text,
                        Email = _registerEmail.Text,
                        PhoneNumber = _registerPhoneNumber.Text,
                        RegisterDateTime = DateTime.Now,
                        LastLoginDateTime = DateTime.Now,
                        Password = Security.Hash(_registerPassword.Text)
                    };
                    usersRepository.Insert(user);

                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    Response.Redirect("~/");
                }
            }
            catch
            {
                Session["AlertType"] = "danger";
                Session["AlertMessage"] = "Er is iets foutgelopen tijdens het registreren";
                return;
            }
		}
	}
}