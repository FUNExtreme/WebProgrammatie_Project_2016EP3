using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace YouthLocationBooking.Web.Code.Auth
{
    public class AuthorizedUser : IPrincipal
    {
        #region Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public IIdentity Identity { get; set; }
        #endregion

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}