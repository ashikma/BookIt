using BookIt;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BookItWebService
{
    /// <summary>
    /// Handles database operations related to products.
    /// </summary>
    public class ProductRepository : Repository, IRepository<Product>
    {
        /// <summary>
        /// Initializes a new instance of the ProductRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="model">The Product object containing product details.</param>
        /// <returns>True if the product was successfully added; otherwise, false.</returns>
        public bool Create(Product model)
        {
            string sql = $@"INSERT INTO Products (Cost, ProductName, NumInStock, Description) " +
                         "values(@cost, @name, @num, @description)";
            this._dbContext.AddParameter("@cost", model.Cost.ToString());
            this._dbContext.AddParameter("@name", model.ProductName);
            this._dbContext.AddParameter("@num", model.NumInStock.ToString());
            this._dbContext.AddParameter("@description", model.Description);

            return this._dbContext.Insert(sql);
        }

        /// <summary>
        /// Deletes a product from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = $@"Delete from Product_Basket where ProductID=@id";
            this._dbContext.AddParameter("@id", id);
            this._dbContext.Delete(sql);
            sql = $@"Delete from Products where ProductID=@id";
            this._dbContext.AddParameter("@id", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A list of Product objects representing all products.</returns>
        public List<Product> GetAll()
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * from Products";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.ProductCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A Product object representing the product.</returns>
        public Product GetById(string id)
        {
            string sql = "Select * from Products where ProductID=@id";
            this._dbContext.AddParameter("@id", id);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                Product product = this.modelFactory.ProductCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return product;
            }
        }

        /// <summary>
        /// Retrieves the last added product.
        /// </summary>
        /// <returns>Throws a NotImplementedException as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="model">The Product object containing updated product details.</param>
        /// <returns>True if the product was successfully updated; otherwise, false.</returns>
        public bool Update(Product model)
        {
            string sql = $@"Update Products SET 
                            Cost=@cost, 
                            NumInStock=@num,    
                            Description=@desc WHERE ProductID=@ID";
            this._dbContext.AddParameter("@ID", model.Id);
            this._dbContext.AddParameter("@cost", model.Cost.ToString());
            this._dbContext.AddParameter("@num", model.NumInStock.ToString());
            this._dbContext.AddParameter("@desc", model.Description);
            return this._dbContext.Update(sql);
        }


        public List<Product> GetProductByPrice(int price)
        {
            List<Product> products = new List<Product>();
            price += 1;
            string sql = "SELECT * from Products WHERE Cost<@price";
            this._dbContext.AddParameter("@price", price.ToString());

            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    products.Add(this.modelFactory.ProductCreator.CreateModel(reader));
                }
            }
            return products;
        }
        public List<Product> GetProductByName(string name)
        {
            List<Product> products = new List<Product>();
            string sql = $@"SELECT * FROM Products WHERE ProductName LIKE '%{name}%'";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    products.Add(this.modelFactory.ProductCreator.CreateModel(reader));
                }
            }
            return products;
        }

        public List<Product> GetProductsByNameAndPrice(int price, string name) 
        {
            List<Product> products = new List<Product>();
            price += 1;
            string sql = $@"SELECT * from Products WHERE ProductName LIKE '%{name}%' AND Cost<@price";
            this._dbContext.AddParameter("@price", price.ToString());
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    products.Add(this.modelFactory.ProductCreator.CreateModel(reader));
                }
            }
            return products;
        }
    }
}
