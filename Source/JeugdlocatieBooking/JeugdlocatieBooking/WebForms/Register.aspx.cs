using System;
using System.Web.Security;

namespace YouthLocationBooking.WebForms
{
    public partial class Register : System.Web.Mvc.ViewPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void registerSubmit_Click(object sender, EventArgs e)
		{
            /*
			Person person = new Person {
				FirstName = this.registerFirstName.Text,
				LastName = this.registerLastName.Text,
				Email = this.registerEmail.Text,
				AddressCountry = "Belgium",
				AddressHouseNumber = this.registerAddressNumber.Text,
				AddressPostcode = Int32.Parse(this.registerAddressPostcode.Text),
				AddressStreet = this.registerAddressStreet.Text,
				AddressTown = this.registerAddressTown.Text,
				Type = personType,
				Password = Security.Hash(this.registerPassword.Text)
			};
            */

			//Person.Insert(person);

			//FormsAuthentication.SetAuthCookie(person.Email, true);
		}
	}
}