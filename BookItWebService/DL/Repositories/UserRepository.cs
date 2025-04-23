using BookIt;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class UserRepository : Repository, IRepository<User>
    {
        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="model">The user model containing user details.</param>
        /// <returns>True if the user was successfully created; otherwise, false.</returns>
        public bool Create(User model)
        {
            //string sql = $@"INSERT INTO Users (UserName, Password, IsManager, PhoneNum, Email, Description) values(@UserName, @Password, @IsManager, @PhoneNumber, @Email, @Description)";
            string sql = $@"INSERT INTO Users (UserName, Password, PhoneNum, Email, Description) values(@UserName, @Password, @PhoneNumber, @Email, @Description)";
            this._dbContext.AddParameter("@UserName", model.UserName);
            this._dbContext.AddParameter("@Password", model.Password);
            //this._dbContext.AddParameter("@IsManager", model.IsManager.ToString());
            this._dbContext.AddParameter("@PhoneNum", model.PhoneNumber);
            this._dbContext.AddParameter("@Email", model.Email);
            this._dbContext.AddParameter("@Description", model.Description);
            return this._dbContext.Insert(sql);
        }

        /// <summary>
        /// Deletes a user from the database based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>True if the user was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = "Delete from Users where UserID=@UserID";
            this._dbContext.AddParameter("@UserId", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of ShoppingBasket objects representing all users.</returns>
        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            string sql = "SELECT * from Users";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.UserCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A user object representing the user.</returns>
        public User GetById(string id)
        {
            string sql = "SELECT * FROM Users WHERE UserID=@UserID";
            this._dbContext.AddParameter("@UserID", id);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                User user = this.modelFactory.UserCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return user;
            }
        }

        /// <summary>
        /// Retrieves a user by their name and password.
        /// </summary>
        /// <param name="name">The name of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>A user object representing the user.</returns>
        public User GetByNamePass(string name, string password)
        {
            string sql = "SELECT * FROM Users WHERE [UserName]=@name AND [Password]=@pass";
            this._dbContext.AddParameter("@name", name);
            this._dbContext.AddParameter("@pass", password);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                User user = this.modelFactory.UserCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return user;
            }
        }

        /// <summary>
        /// Throws a NotImplementedException for retrieving the last user.
        /// </summary>
        /// <returns>Throws an exception as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a user's details in the database.
        /// </summary>
        /// <param name="model">The ing user model containing updated user details.</param>
        /// <returns>True if the user was successfully updated; otherwise, false.</returns>
        public bool Update(User model)
        {
            string sql = @"UPDATE Users SET 
                 UserName=@UserName, 
                 Password=@Password,    
                 PhoneNum=@PhoneNumber,
                 Email=@Email,
                 Description=@Description 
                 WHERE UserID=@userId";
            this._dbContext.AddParameter("@Password", model.Password);
            this._dbContext.AddParameter("@PhoneNumber", model.PhoneNumber);
            this._dbContext.AddParameter("@Email", model.Email);
            //this._dbContext.AddParameter("@IsManage", model.IsManager.ToString());
            this._dbContext.AddParameter("@userId", model.Id);
            return this._dbContext.Update(sql);
        }

        /// <summary>
        /// Retrieves the manager associated with a specific appointment.
        /// </summary>
        /// <param name="apointmentID">The ID of the appointment.</param>
        /// <returns>A user object representing the manager.</returns>
        public User GetManagerByAppointment(string apointmentID)
        {
            string sql = "SELECT Users.UserName" +
                         "FROM Users INNER JOIN Apointments ON Users.UserID = Apointments.managerID" +
                         "WHERE (((Apointments.ApointmentID)=@id));";
            this._dbContext.AddParameter("@id", apointmentID);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                User manager = this.modelFactory.UserCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return manager;
            }
        }

        public List<User> GetAllManagers()
        {
            List<User> list = new List<User>();
            string sql = "SELECT Users.* FROM Users INNER JOIN Managers ON Users.UserID = Managers.ID;";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.UserCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public JsonResult GetFullyBookedDays(int managerID = 1)
        {
            List<string>  list = new List<string>();
            string sql = "SELECT Apointments.Date FROM Apointments WHERE Apointments.ManagerId = {managerID}  GROUP BY Apointments.Date HAVING COUNT(*) = 3;";
            this._dbContext.AddParameter("@managerID", managerID.ToString());
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    string rawDate = reader.GetString(0);
                    if (DateTime.TryParseExact(rawDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        // format it as "yyyy-MM-dd" like "2024-10-20"
                        list.Add(parsedDate.ToString("yyyy-MM-dd"));
                    }
                }
            }
            return new JsonResult(list);
        }
    }
}
