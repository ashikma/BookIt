using BookIt;
using Microsoft.AspNetCore.Mvc;
using WebApiClient;

namespace BookItWebApplication.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseTender()
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetSchedule";
            string  json = webClient.Get().Result;
            var managers = Newtonsoft.Json.JsonConvert.DeserializeObject<string[,]>(json);
            ViewBag.TreatmentID = "10";
            ViewBag.UserID = "11";
            return View(managers);
        }

        public JsonResult GetFullyBookedDates()
        {
            WebClient<JsonResult> webClient = new WebClient<JsonResult>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetFullyBookedDates";
            return webClient.Get().Result;
        }

        public IActionResult ChooseTime()
        {
            WebClient<List<string>> webClient = new WebClient<List<string>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetTender";
            return View();
        }
    }
}
