using BookIt;
using BookItModels.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookItWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class UserController : ControllerBase
    {

        DbContext db;
        UnitOfWorkRepository unitOfWork;
        GuestController guestController;

        public UserController()
        {
            this.db = DbContext.GetInstance();
            this.unitOfWork = new UnitOfWorkRepository(this.db);
            this.guestController = new GuestController();
        }

        [HttpGet]
        ///return id of user if is in the system
        ///else: return null
        public string? SingIn(string username, string password)
        {
            try
            {
                this.db.OpenConnection();
                User user = unitOfWork.UserRepository.GetByNamePass(username, password);
                if (user == null)
                {
                    return null;
                }
                return user.Id; // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        //[HttpGet] //fix!
        //public GetAppointmentView GetAppointmentViewModel()
        //{
        //    GetAppointmentView viewModel = new GetAppointmentView();
        //    return viewModel;
        //}

        [HttpGet]
        public List<User>? GetTender()
        {
            try
            {
                this.db.OpenConnection();
                List<User> managers = unitOfWork.UserRepository.GetAllManagers();
                if (managers == null)
                {
                    return null;
                }
                return managers;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.db.CloseConnection();
            }

        }

        [HttpGet]
       public bool ScheduleTreatment(string treatmentID, string userID, string date,
                                        string time, string duration)
        {
            try
            {
                this.db.OpenConnection();
                bool ok= unitOfWork.AppointmentRepository.AddAppoiment(treatmentID, userID,
                                                                       date,time, duration);
                return ok; 
            }
            catch (Exception ex) {
                return false;
            }
            finally
            {
                this.db.CloseConnection();
            }
        }


            [HttpGet]
        public string GetSchedule(string start = "23/04/2025", string finish = "29/04/2025")
        {
            List<Appointment> appointments = null;
            try
            {
                this.db.OpenConnection();
                appointments = unitOfWork.AppointmentRepository.GetAppointmentsByDate(start, finish);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.db.CloseConnection();
            }
            DateTime startDate = DateTime.ParseExact(start, "dd/MM/yyyy", null);
            DateTime finishDate = DateTime.ParseExact(finish, "dd/MM/yyyy", null);
            TimeSpan timeSpan = finishDate - startDate;
            int days = timeSpan.Days + 1;
            string[,] workSchedule = GetTimes(days);
            foreach (Appointment appointment in appointments)
            {
                DateTime appointmentDate = DateTime.ParseExact(appointment.Date + " " + appointment.Oclock, "dd/MM/yyyy HH:mm", null); ;
                int day = appointmentDate.Day - startDate.Day;
                int hour = appointmentDate.Hour;
                int minutes = appointmentDate.Minute;
                int index = (hour - 9) * 2 + (minutes / 30);
                int duration = (int)(Convert.ToDouble(appointment.Time) * 60) / 30; ;
                for (int i = 0; i < duration; i++)
                {
                    workSchedule[day, index + i] = "";
                }
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(workSchedule);
            Print(workSchedule);
            return json;
        }

        void Print(string[,] args)
        {
            for (int i = 0; i < args.GetLength(0); i++)
            {
                for (int j = 0; j < args.GetLength(1); j++)
                    Console.Write(args[i, j] + " ");
                Console.WriteLine();
            }
        }
        private string[,] GetTimes(int days)
        {
            int startHour = 9;
            int finishHour = 19;
            string[,] times = new string[days, (finishHour - startHour) * 2];
            int minutes = 0;
            for (int i = 0; i < times.GetLength(0); i++)
            {
                int start = startHour;
                for (int j = 0; j < times.GetLength(1); j++)
                {
                    if (start < 10)
                    {
                        if (minutes == 0)
                            times[i, j] = $"0{start}:0{minutes}";
                        else
                            times[i, j] = $"0{start}:{minutes}";
                    }
                    else
                    {
                        if (minutes == 0)
                            times[i, j] = $"{start}:0{minutes}";
                        else
                            times[i, j] = $"{start}:{minutes}";
                    }
                    minutes += 30;
                    if (minutes == 60)
                    {
                        minutes = 0;
                        start++;
                    }
                }

            }
            return times;
        }

        [HttpGet]
        public JsonResult GetFullyBookedDates()
        {
            try
            {
                this.db.OpenConnection();
                //get full days from db
                JsonResult fullyBookedDays = unitOfWork.UserRepository.GetFullyBookedDays();
                return fullyBookedDays;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult MakeAnAppointment(Appointment appointment)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.AppointmentRepository.Create(appointment);
                return Ok("User successfully made an appointment."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult DelAppointment(string id)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.AppointmentRepository.Delete(id);
                return Ok("User successfully deleted an appointment."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpGet]
        public IActionResult GetUserBasket(string basketID)
        {
            try
            {
                this.db.OpenConnection();
                ShoppingBasket b = unitOfWork.ShoppingBasketRepository.GetById(basketID);
                return Ok(b); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult Pay(ShoppingBasket basket)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.ShoppingBasketRepository.Update(basket);
                return Ok("User successfully payed on the basket."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult addProductToBasket(Product product, string basketID, int num)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.ShoppingBasketRepository.AddProduct(product, basketID, num);
                return Ok("User successfully added product to the basket."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult DelProduct(Product product, string basketID)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.ShoppingBasketRepository.DelProduct(product, basketID);
                return Ok("User successfully deleted product to the basket."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

        [HttpPost]
        public IActionResult updateAmount(Product product, string basketID, int num)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.ShoppingBasketRepository.updateAmount(product, basketID, num);
                return Ok("User successfully updated num  of  product to the basket."); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }

    }
}

