using BookIt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace BookItWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        DbContext db;
        UnitOfWorkRepository unitOfWork;
        UserController userController;

        public ManagerController()
        {
            this.db = DbContext.GetInstance();
            this.unitOfWork = new UnitOfWorkRepository(this.db); ;
            this.userController = new UserController();
        }

        [HttpPost]
        public bool CancelAppointment(Appointment appointment)
        {
            try
            {
                this.db.OpenConnection();
                return unitOfWork.AppointmentRepository.Delete(appointment.Id);
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


        //need?????????????

        //[HttpPost]
        //public IActionResult AddProduct(Product product)
        //{
        //    try
        //    {
        //        this.db.OpenConnection();
        //        unitOfWork.ProductRepository.Create(product);
        //        return Ok("Manager successfully added product"); // Return success message with 200 OK status
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        this.db.CloseConnection();
        //    }
        //}

        //[HttpPost]
        //public IActionResult DelProduct(Product product)
        //{
        //    try
        //    {
        //        this.db.OpenConnection();
        //        unitOfWork.ProductRepository.Delete(product.Id);
        //        return Ok("Manager successfully deleted product"); // Return success message with 200 OK status
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        this.db.CloseConnection();
        //    }
        //}

        //[HttpPost]
        //public IActionResult UpdateProduct(Product product)
        //{
        //    try
        //    {
        //        this.db.OpenConnection();
        //        unitOfWork.ProductRepository.Update(product);
        //        return Ok("Manager successfully updated product"); // Return success message with 200 OK status
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        this.db.CloseConnection();
        //    }
        //}

        [HttpPost]
        public IActionResult AddWork()
        {
            string json = Request.Form["model"];
            IFormFile file = Request.Form.Files[0];
            Work work = JsonSerializer.Deserialize<Work>(json);
            try
            {
                this.db.OpenConnection();
                this.db.BeginTransaction();
                if(unitOfWork.WorkRepository.Create(work))
                {
                    string path = Directory.GetCurrentDirectory() + $@"\wwwroot\IMG\{work.ImgName}";
                    using(Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }

                }
                this.db.Commit();
                return Ok("Manager successfully added work"); // Return success message with 200 OK status
            }
            catch (Exception ex)
            {
                this.db.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
            finally
            {
                this.db.CloseConnection();
            }
        }
        //[HttpPost]
        //public IActionResult AddWork(Work work, IFormFile file)
        //{
        //    try
        //    {
        //        this.db.OpenConnection();
        //        unitOfWork.WorkRepository.Create(work);
        //        return Ok("Manager successfully added work"); // Return success message with 200 OK status
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        this.db.CloseConnection();
        //    }
        //}

        [HttpPost]
        public IActionResult DelWork(Work work)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.WorkRepository.Delete(work.Id);
                return Ok("Manager successfully deleted work"); // Return success message with 200 OK status
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
        public IActionResult AddTreatment(Treatment treatment)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.TreatmentRepository.Create(treatment);
                return Ok("Manager successfully added treatment"); // Return success message with 200 OK status
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
        public IActionResult DelTreatment(Treatment treatment)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.TreatmentRepository.Delete(treatment.Id);
                return Ok("Manager successfully deleted treatment"); // Return success message with 200 OK status
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
        public IActionResult UpdateTreatment(Treatment treatment)
        {
            try
            {
                this.db.OpenConnection();
                unitOfWork.TreatmentRepository.Update(treatment);
                return Ok("Manager successfully updated treatment"); // Return success message with 200 OK status
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
