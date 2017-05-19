using MyCoolWebApp.Data;
using MyCoolWebApp.Services;
using MyCoolWebApp.ViewModels;
using System.Configuration;
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
            var reader = new AppSettingsWebConfigReader();
            var vm = new IndexViewModel();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            var blobStorageConnectionString = reader.ReadProperty("AzureStorageConnectionString");

            if (connectionString != null)
            {
                vm.ConncetionString = connectionString.ConnectionString;
                vm.BlobStorageConnectionString = blobStorageConnectionString;
            }

            return View(vm);
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