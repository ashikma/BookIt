using BookIt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Xml.Linq;

namespace BookItWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        DbContext db;
        UnitOfWorkRepository unitOfWork;

        public GuestController()
        {
            this.db = DbContext.GetInstance();
            this.unitOfWork = new UnitOfWorkRepository(this.db);
        }

        [HttpGet]
        public IActionResult GetTreatments(string name = "")
        {
            try
            {
                List<Treatment> treatments = new List<Treatment>();
                this.db.OpenConnection();
                treatments = unitOfWork.TreatmentRepository.GetAll();
                if (name != "")
                {
                    treatments = unitOfWork.TreatmentRepository.GetTreatmentByName(name);
                }
                return Ok(treatments); // Return treatments with 200 OK status
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
        public IActionResult GetWorks()
        {
            try
            {
                List<Work> works = new List<Work>();
                this.db.OpenConnection();
                works = unitOfWork.WorkRepository.GetAll();
                return Ok(works); // Return treatments with 200 OK status
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
        public IActionResult GetProducts(int price = 0, string name = "")
        {
            try
            {
                List<Product> products = new List<Product>();
                this.db.OpenConnection();

                products = unitOfWork.ProductRepository.GetAll();
                if(price > 0)
                {
                    products = unitOfWork.ProductRepository.GetProductByPrice(price);
                }
                if(name != "")
                {
                    products = unitOfWork.ProductRepository.GetProductByName(name);
                }
                if (price > 0 && name != "")
                {
                    products = unitOfWork.ProductRepository.GetProductsByNameAndPrice(price, name);
                }
                return Ok(products); // Return products with 200 OK status
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
        protected bool SignUp(string email, string phone, string name, string password, string discription)
        {
            User user = new User()
            {
                Description = discription,
                Email = email,
                Password = password,
                PhoneNumber = phone,
                UserName = name,
                Id = "0"
            };

            try
            {
                this.db.OpenConnection();
                unitOfWork.UserRepository.Create(user);
                return true; // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.db.CloseConnection();
            }
        }
    }
}
