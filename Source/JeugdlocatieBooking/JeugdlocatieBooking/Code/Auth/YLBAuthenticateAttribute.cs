using System.Web;
using System.Web.Mvc;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Database.Repositories;

namespace YouthLocationBooking.Web.Code.Auth
{
    public class YLBAuthenticateAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if ((httpContext.User == null) || (!httpContext.User.Identity.IsAuthenticated))
                return false;

            UnitOfWork unitOfWork = new UnitOfWork();
            UsersRepository usersRepository = unitOfWork.UsersRepository;
            DbUser user = usersRepository.GetByEmail(httpContext.User.Identity.Name);
            if (user == null)
                return false;

            
            AuthorizedUser authorizedUser = new AuthorizedUser();
            authorizedUser.Id = user.Id;
            authorizedUser.Email = user.Email;
            authorizedUser.FirstName = user.FirstName;
            authorizedUser.LastName = user.LastName;
            authorizedUser.Identity = httpContext.User.Identity;
            // We override User with our own type, this way we don't need to re-retrieve the user when we need to verify authorization
            httpContext.User = authorizedUser;

            return true;
        }
    }
}