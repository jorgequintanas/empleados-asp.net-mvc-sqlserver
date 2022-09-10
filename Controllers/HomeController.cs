using System.Web.Mvc;

namespace Examen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página Principal.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto.";

            return View();
        }
    }
}