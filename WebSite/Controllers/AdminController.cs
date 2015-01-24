using System.Web.Mvc;

namespace WebSite.Controllers
{
    public partial class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}