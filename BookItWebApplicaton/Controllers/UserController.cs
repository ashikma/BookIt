using BookIt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Xml.Linq;
using WebApiClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookItWebApplication.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ViewLoginForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/SingIn";
            webClient.AddParam("username", username);
            webClient.AddParam("password", password);

            string userId = webClient.Get().Result;
            if (userId == "" || userId == null)
            {
                ViewBag.Error = "username or password incorrect";
                return View("viewLoginForm");
            }
            HttpContext.Session.SetString("userID", userId);//add the user id to the session
                                                            //session=task with info about browser 

            return RedirectToAction("GetTreatments", "Catalog");

        }

        public IActionResult ViewSignupForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string email, string phone, string name, string password, string discription)
        {
            
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/SignUp";
            webClient.AddParam("email", email);
            webClient.AddParam("phone", phone);
            webClient.AddParam("name", name);
            webClient.AddParam("password", password);
            webClient.AddParam("discription", discription);

            string isOK = webClient.Get().Result;
            if (isOK != null)
            {
                ViewBag.error = "something went wrong... try again later";
                return View("ViewSignupForm");
            }

            return View("ViewLoginForm");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userID");//del the user id from the session
            return RedirectToAction("GetWorksCatalog", "Catalog");//get back to the home page
        }

       


    }
}
