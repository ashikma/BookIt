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
