using BookItWebService;
using System.Data;
using System.Data.OleDb;

namespace BookItWebService
{
    public class DbContext : IDbContext
    {
        private static DbContext dbContext;
        OleDbCommand command;
        OleDbConnection connection;
        OleDbTransaction transaction;

        public static DbContext GetInstance()
        {
            if (dbContext == null)
                dbContext = new DbContext();
            return dbContext;
        }

        // Constructor
        /// <summary>
        /// The constructor initializes a new instance of the DbContext class, setting up the OleDbConnection
        /// with the connection string to the Access database file.
        /// </summary>
        public DbContext()
        {
            this.connection = new OleDbConnection();
            this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\App_Data\BookItDatabase.accdb";
            this.command = new OleDbCommand();
            this.command = this.connection.CreateCommand();
        }


        /// <summary>
        /// Function that closes the database connection.
        /// It also disposes of any active transaction if it exists.
        /// </summary>
        public void CloseConnection()
        {
            this.connection.Close();
            this.command.Parameters.Clear();
            if (this.transaction != null)
            {
                this.transaction.Dispose();
            }
        }

        /// <summary>
        /// Function that commits the changes to the database.
        /// This is used to finalize a transaction and apply the changes to the database.
        /// </summary>
        public void Commit()
        {
            this.transaction.Commit();
        }

        /// <summary>
        /// Function that deletes a row from a table based on a specific SQL query.
        /// </summary>
        /// <param name="sql">The SQL query string to delete data from the database.</param>
        /// <returns>Returns true if the data was successfully deleted, otherwise false.</returns>
        public bool Delete(string sql)
        {
            return ChangeDB(sql);
        }

        /// <summary>
        /// Function that inserts a new row into a table using a specified SQL query.
        /// </summary>
        /// <param name="sql">The SQL query string to insert data into the database.</param>
        /// <returns>Returns true if the data was successfully inserted, otherwise false.</returns>
        public bool Insert(string sql)
        {
            return ChangeDB(sql);
        }

        /// <summary>
        /// Function that opens the database connection.
        /// This needs to be called before performing any database operations.
        /// </summary>
        public void OpenConnection()
        {
            this.connection.Open();
        }

        /// <summary>
        /// Function that reads data from the database based on a specific SQL query.
        /// Executes a SELECT query and returns a data reader for the caller to access the results.
        /// </summary>
        /// <param name="sql">The SQL query string to retrieve data from the database.</param>
        /// <returns>A data reader containing the result set of the query.</returns>
        public IDataReader Read(string sql)
        {
            this.command.CommandText = sql;
            return this.command.ExecuteReader();
        }

        /// <summary>
        /// Function that reads a single value from the database based on a specific SQL query.
        /// This is typically used for queries like SELECT COUNT(), SELECT MAX(), etc., which return a single value.
        /// </summary>
        /// <param name="sql">The SQL query string to retrieve a single value from the database.</param>
        /// <returns>The result of the query as an object (could be null or any data type based on the query).</returns>
        public object ReadValue(string sql)
        {
            this.command.CommandText = sql;
            return this.command.ExecuteScalar();
        }

        /// <summary>
        /// Function that rolls back a transaction.
        /// This functionality is currently not implemented, but it would be used to revert changes in case of an error.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
        }

        /// <summary>
        /// Function that updates a row in a table based on a specific SQL query.
        /// </summary>
        /// <param name="sql">The SQL query string to update data in the database.</param>
        /// <returns>Returns true if the data was successfully updated, otherwise false.</returns>
        public bool Update(string sql)
        {
            return ChangeDB(sql);
        }

        /// <summary>
        /// Private helper function that executes any SQL query that modifies the database (INSERT, DELETE, UPDATE).
        /// It returns true if the query affected at least one row, otherwise false.
        /// </summary>
        /// <param name="sql">The SQL query string to execute (INSERT, DELETE, UPDATE).</param>
        /// <returns>Returns true if the query affected one or more rows, otherwise false.</returns>
        private bool ChangeDB(string sql)
        {
            this.command.CommandText = sql;
            bool ok = this.command.ExecuteNonQuery() > 0;
            ClearParameters();
            return ok;

        }

        /// <summary>
        /// func that adding parameter to the command.
        /// </summary>
        /// <param name="name">the name of the parameter to add</param>
        /// <param name="value">the value of the parameter to add</param>
        public void AddParameter(string name, string value)
        {
            this.command.Parameters.Add(new OleDbParameter(name, value));
        }

        public void ClearParameters()
        { this.command.Parameters.Clear(); }


        public void BeginTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
        }
    }
}
