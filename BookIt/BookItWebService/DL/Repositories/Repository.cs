

namespace BookItWebService
{
    public class Repository
    {
        protected DbContext _dbContext;
        protected ModelFactory modelFactory;

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.modelFactory = new ModelFactory();
        }

        public string GetLastID()
        {
            string sql = "Select @@Identity";
            return this._dbContext.ReadValue(sql).ToString();
        }
    }
}
