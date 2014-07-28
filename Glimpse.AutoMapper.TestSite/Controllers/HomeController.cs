using System.Web.Mvc;

namespace Glimpse.AutoMapper.TestSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}