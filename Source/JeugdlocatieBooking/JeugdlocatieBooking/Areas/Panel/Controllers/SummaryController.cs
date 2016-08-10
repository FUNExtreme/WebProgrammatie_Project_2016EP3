using System.Web.Mvc;
using YouthLocationBooking.Web.Code.Auth;

namespace YouthLocationBooking.Web.Areas.Panel.Controllers
{
    [YLBAuthenticateAttribute]
    public class SummaryController : Controller
    {
        // GET: Panel/Summary
        public ActionResult Index()
        {
            return View();
        }
    }
}