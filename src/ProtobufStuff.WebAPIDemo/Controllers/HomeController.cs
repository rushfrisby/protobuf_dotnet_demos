using System.Web.Mvc;

namespace ProtobufStuff.WebAPIDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Go to <a href='/api/now'>/api/now</a>", "text/html");
        }
    }
}
