using MyCoolWebApp.Data;
using MyCoolWebApp.Services;
using System.Linq;
using System.Web.Mvc;

namespace MyCoolWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlobStorageService m_blobStorageService;
        public HomeController(IBlobStorageService blobStorageService)
        {
            m_blobStorageService = blobStorageService;
        }

        public ActionResult Index()
        {
            using (var context = new MyCoolWebAppContext())
            {
                var data = context.Products.ToList();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}