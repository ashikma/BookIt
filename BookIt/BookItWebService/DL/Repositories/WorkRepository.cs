using BookIt;
using System.Data;

namespace BookItWebService
{
    public class WorkRepository : Repository, IRepository<Work>
    {
        /// <summary>
        /// Initializes a new instance of the WorkRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public WorkRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new work record in the database.
        /// </summary>
        /// <param name="model">The Work object containing work details.</param>
        /// <returns>True if the work record was successfully created; otherwise, false.</returns>
        public bool Create(Work model)
        {
            string sql = $@"INSERT INTO MyWorks (Cost, ImgName, [Date], [Time], treatmentID) 
                          values(@cost, @imgName, @date, @time, @treatmentId)";

            this._dbContext.AddParameter("@cost", model.Cost.ToString());
            this._dbContext.AddParameter("@imgName", model.ImgName);
            this._dbContext.AddParameter("@date", model.Date);
            this._dbContext.AddParameter("@time", model.Time);
            this._dbContext.AddParameter("@treatmentId", model.treatmentID.ToString());
            return this._dbContext.Insert(sql);
        
        }


        /// <summary>
        /// Deletes a work record from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the work record to be deleted.</param>
        /// <returns>True if the work record was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = "DELETE FROM MyWorks where WorkID=@workID";
            this._dbContext.AddParameter("@workID", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all work records from the database.
        /// </summary>
        /// <returns>A list of Work objects representing all work records.</returns>
        public List<Work> GetAll()
        {
            List<Work> list = new List<Work>();
            string sql = "SELECT * from Works";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.WorkCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves a work record by its ID.
        /// </summary>
        /// <param name="id">The ID of the work record to retrieve.</param>
        /// <returns>A Work object representing the work record.</returns>
        public Work GetById(string id)
        {
            string sql = "SELECT * from MyWorks where WorkID=@workID";
            this._dbContext.AddParameter("@workID", id);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                Work work = this.modelFactory.WorkCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return work;
            }
        }

        /// <summary>
        /// Throws a NotImplementedException for retrieving the last work record.
        /// </summary>
        /// <returns>Throws an exception as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a work record in the database.
        /// </summary>
        /// <param name="model">The Work object containing updated work details.</param>
        /// <returns>Throws a NotImplementedException as the method is not implemented.</returns>
        public bool Update(Work model)
        {
            throw new NotImplementedException();
        }
    }
}
