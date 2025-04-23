using BookIt;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class AppointmentRepository : Repository, IRepository<Appointment>
    {
        /// <summary>
        /// Initializes a new instance of the AppointmentRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public AppointmentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new appointment in the database.
        /// </summary>
        /// <param name="model">The Appointment object containing appointment details.</param>
        /// <returns>True if the appointment was successfully created; otherwise, false.</returns>
        public bool Create(Appointment model)
        {
            string sql = $@"INSERT INTO Apointments (userID, [Time], Oclock, TreatmentID, Cost, ManagerId) " +
                         "values(@userID, @time, @oclock, @treatmentID, @cost, @managerID)";
            this._dbContext.AddParameter("@userID", model.CostumerId.ToString());
            this._dbContext.AddParameter("@time", model.Time);
            this._dbContext.AddParameter("@oclock", model.Oclock);
            this._dbContext.AddParameter("@treatmentID", model.TreatmentId.ToString());
            this._dbContext.AddParameter("@cost", model.Cost.ToString());
            this._dbContext.AddParameter("@managerID", model.ManagerID);

            return this._dbContext.Insert(sql);
        }

        /// <summary>
        /// Deletes an appointment from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to be deleted.</param>
        /// <returns>True if the appointment was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = $@"DELETE FROM Apointments where ApointmentID=@id";
            this._dbContext.AddParameter("@id", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all appointments from the database.
        /// </summary>
        /// <returns>A list of Appointment objects representing all appointments.</returns>
        public List<Appointment> GetAll()
        {
            List<Appointment> list = new List<Appointment>();
            string sql = "Get * from Appointments";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.AppointmentCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>An Appointment object representing the appointment.</returns>
        public Appointment GetById(string id)
        {
            string sql = $@"Select * from Appointments where ApointmentID=@id";
            this._dbContext.AddParameter("@id", id);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                Appointment appointment = this.modelFactory.AppointmentCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return appointment;
            }
        }

        /// <summary>
        /// Throws a NotImplementedException for retrieving the last appointment.
        /// </summary>
        /// <returns>Throws an exception as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an appointment in the database.
        /// </summary>
        /// <param name="model">The Appointment object containing updated details.</param>
        /// <returns>Throws a NotImplementedException as the method is not implemented.</returns>
        public bool Update(Appointment model)
        {
            throw new NotImplementedException();
        }
    }
}
