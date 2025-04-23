using BookIt;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;
using System.Xml.Linq;
using WebApiClient;

namespace BookItWebApplication.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWorksCatalog() 
        {
            WebClient<List<Work>> webClient = new WebClient<List<Work>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/GetWorks";
            List<Work> works = webClient.Get().Result;
            return View(works); 
        }

        public IActionResult GetTreatments(string name = null)
        {
            WebClient<List<Treatment>> webClient = new WebClient<List<Treatment>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/GetTreatments";
            if(name != null)
                webClient.AddParam("name", name);
            List<Treatment> treatments = webClient.Get().Result;
            return View(treatments);
        }

        [HttpPost]
        public IActionResult AddWork(Work work, IFormFile file)
        {
            WebClient<Work> webClient = new WebClient<Work>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Manager/AddWork";
            bool ok = webClient.Post(work, file.OpenReadStream()).Result;
            return View();
        }

    }
}
