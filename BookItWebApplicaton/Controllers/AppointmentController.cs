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

        public IActionResult ChooseTender(string treatmentId="10",string duration="1")
        {
            var managers = GetAviableTime();
            ViewBag.TreatmentID = treatmentId;
            ViewBag.UserID = "11";//session
            ViewBag.Duration = duration;
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

        public PartialViewResult ScheduleTreatment(string treatmentID, string userID, string date, string time,string duration)
        {
            WebClient<bool> webClient = new WebClient<bool>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/ScheduleTreatment";
            webClient.AddParam("treatmentID", treatmentID);
            webClient.AddParam("userID", userID);
            webClient.AddParam("date", date);
            webClient.AddParam("time", time);
            webClient.AddParam("duration", duration);
            bool isOK = webClient.Get().Result;
            string[,] managers = null;
            if (isOK)
            {
                managers = GetAviableTime();
                ViewBag.Message = "Your appointment has been scheduled successfully.";
            }
            else
            {
                ViewBag.Message = "Failed to schedule your appointment. Please try again.";
            }

            return PartialView(managers);
        }
        private string[,] GetAviableTime()
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetSchedule";
            string json = webClient.Get().Result;
            var managers = Newtonsoft.Json.JsonConvert.DeserializeObject<string[,]>(json);
            return managers;
        }
    }
}
