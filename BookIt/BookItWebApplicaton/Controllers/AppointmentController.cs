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
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetTender";
            List<User> managers = webClient.Get().Result;
            return View(managers);
        }

        public IActionResult ChooseDate()
        {
            WebClient<List<User>> webClient = new WebClient<List<User>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/GetTender";
            return View();
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
