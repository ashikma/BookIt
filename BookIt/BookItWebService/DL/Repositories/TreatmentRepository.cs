using BookIt;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class TreatmentRepository : Repository, IRepository<Treatment>
    {
        /// <summary>
        /// Initializes a new instance of the TreatmentRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public TreatmentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new treatment in the database.
        /// </summary>
        /// <param name="model">The Treatment object containing treatment details.</param>
        /// <returns>True if the treatment was successfully created; otherwise, false.</returns>
        public bool Create(Treatment model)
        {
            string sql = $@"INSERT INTO Treatments (TreatmentName, CostRange, TimeRange, Explanation) " +
                "values(@name, @costR, @timeR, @expl)";
            this._dbContext.AddParameter("@name", model.TreatmentName);
            this._dbContext.AddParameter("@costR", model.CostRange);
            this._dbContext.AddParameter("@timeR", model.TimeRange);
            this._dbContext.AddParameter("@expl", model.Explanation);

            return this._dbContext.Insert(sql);
        }

        /// <summary>
        /// Deletes a treatment from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the treatment to be deleted.</param>
        /// <returns>True if the treatment was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = $@"Delete from Treatments where TreatmentID=@id";
            this._dbContext.AddParameter("@id", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all treatments from the database.
        /// </summary>
        /// <returns>A list of Treatment objects representing all treatments.</returns>
        public List<Treatment> GetAll()
        {
            List<Treatment> list = new List<Treatment>();
            string sql = "SELECT * from Treatments";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.TreatmentCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves a treatment by its ID.
        /// </summary>
        /// <param name="id">The ID of the treatment to retrieve.</param>
        /// <returns>A Treatment object representing the treatment.</returns>
        public Treatment GetById(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws a NotImplementedException for retrieving the last treatment.
        /// </summary>
        /// <returns>Throws an exception as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a treatment's details in the database.
        /// </summary>
        /// <param name="model">The Treatment object containing updated treatment details.</param>
        /// <returns>True if the treatment was successfully updated; otherwise, false.</returns>
        public bool Update(Treatment model)
        {
            string sql = $@"Update Treatments SET 
                 TreatmentName=@name, 
                 CostRange=@cost,    
                 TimeRange=@timeR,
                 Explanation=@expl WHERE TreatmentID=@id";
            this._dbContext.AddParameter("@name", model.TreatmentName);
            this._dbContext.AddParameter("@cost", model.CostRange);
            this._dbContext.AddParameter("@timeR", model.TimeRange);
            this._dbContext.AddParameter("@expl", model.Explanation);
            this._dbContext.AddParameter("@id", model.Id);
            return this._dbContext.Update(sql);
        }

        public List<Treatment> GetTreatmentByName(string  name)
        {
            List<Treatment> list = new List<Treatment>();
            string sql = $@"SELECT * FROM Treatments WHERE TreatmentName LIKE '%{name}%'";
            //this._dbContext.AddParameter("@name", $"%{name}%");
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.TreatmentCreator.CreateModel(reader));
                }
            }
            return list;
        }
    }
}
