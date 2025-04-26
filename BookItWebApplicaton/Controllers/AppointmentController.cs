using BookIt;
using BookItModels.viewModel;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
            ViewBag.UserID = "8";//session
            ViewBag.Duration = duration;
            return View(managers);
        }
        private string[,] GetAviableTime()
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            string start = DateTime.Today.ToString("dd/MM/yyyy");
            string finish = DateTime.Today.AddDays(6).ToString("dd/MM/yyyy");
            webClient.Path = $@"api/User/GetSchedule";
            webClient.AddParam("start", start);
            webClient.AddParam("finish", finish);
            string json = webClient.Get().Result;
            var schedule = Newtonsoft.Json.JsonConvert.DeserializeObject<string[,]>(json);
            return schedule;
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

        public IActionResult ScheduleTreatment(string treatmentID, string userID, string date, string time,string duration)
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

            return RedirectToAction("ChooseTender", "Appointment");
        }

        public PartialViewResult GetApoimentDetals(string treatmentID, string date, string time)
        {
            WebClient<AppoimentDetals> webClient = new WebClient<AppoimentDetals>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetApoimentDetals";
            webClient.AddParam("treatmentID", treatmentID);
            webClient.AddParam("date", date);
            webClient.AddParam("time", time);
            AppoimentDetals appoimentDetals= webClient.Get().Result;
            return PartialView(appoimentDetals);
        }
       
    }
}
