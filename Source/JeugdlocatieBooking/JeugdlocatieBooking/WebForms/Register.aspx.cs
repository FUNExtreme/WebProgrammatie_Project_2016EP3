using System;
using System.Web.Security;
using YouthLocationBooking.Business.Logic.Repositories;
using YouthLocationBooking.Business.Logic.Utils;
using YouthLocationBooking.Data.Database.Models;

namespace YouthLocationBooking.Web.WebForms
{
    public partial class Register : System.Web.Mvc.ViewPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void registerSubmit_Click(object sender, EventArgs e)
		{
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

            using (var repository = new UsersRepository())
            {
                repository.Add(user);
            }

            FormsAuthentication.SetAuthCookie(user.Email, true);
            Response.Redirect("~/");
		}
	}
}