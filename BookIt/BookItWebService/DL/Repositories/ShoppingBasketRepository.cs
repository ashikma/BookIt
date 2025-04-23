using BookIt;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace BookItWebService
{
    public class ShoppingBasketRepository : Repository, IRepository<ShoppingBasket>
    {
        /// <summary>
        /// Initializes a new instance of the ShoppingBasketRepository class.
        /// </summary>
        /// <param name="dbContext">The database context to interact with the database.</param>
        public ShoppingBasketRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Creates a new shopping basket record in the database.
        /// </summary>
        /// <param name="model">The ShoppingBasket object containing basket details.</param>
        /// <returns>True if the shopping basket was successfully created; otherwise, false.</returns>
        public bool Create(ShoppingBasket model)
        {
            string sql = $@"INSERT INTO ShoppingBasket (UserID, TotalCost, AddresToDeliver, Location, IsPayed)" +
                         "VALUES(@id, @cost, @addr, @location, @isPayed)";
            this._dbContext.AddParameter("@id", model.UserID);
            this._dbContext.AddParameter("@cost", model.TotalCost.ToString());
            this._dbContext.AddParameter("@addr", model.AddrId);
            this._dbContext.AddParameter("@location", model.CurrLocation);
            this._dbContext.AddParameter("@isPayed", model.IsPayed.ToString());
            return this._dbContext.Insert(sql);
        }

        /// <summary>
        /// Deletes a shopping basket record from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping basket to delete.</param>
        /// <returns>True if the shopping basket was successfully deleted; otherwise, false.</returns>
        public bool Delete(string id)
        {
            string sql = $@"Delete from ShoppingBaskets where BasketID=@id";
            this._dbContext.AddParameter("@id", id);
            return this._dbContext.Delete(sql);
        }

        /// <summary>
        /// Retrieves all shopping basket records from the database.
        /// </summary>
        /// <returns>A list of ShoppingBasket objects representing all shopping baskets.</returns>
        public List<ShoppingBasket> GetAll()
        {
            List<ShoppingBasket> list = new List<ShoppingBasket>();
            string sql = "Get * from ShoppingBaskets";
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(this.modelFactory.ShoppingBasketCreator.CreateModel(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// Retrieves a shopping basket by its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping basket to retrieve.</param>
        /// <returns>A ShoppingBasket object representing the shopping basket.</returns>
        public ShoppingBasket GetById(string id)
        {
            string sql = $@"SELECT * FROM ShoppingBaskets where BasketID=@id";
            this._dbContext.AddParameter("@id", id);
            using (IDataReader reader = this._dbContext.Read(sql))
            {
                reader.Read();
                ShoppingBasket basket = this.modelFactory.ShoppingBasketCreator.CreateModel(reader);
                this._dbContext.ClearParameters();
                return basket;
            }
        }

        /// <summary>
        /// Retrieves the last shopping basket record.
        /// </summary>
        /// <returns>Throws a NotImplementedException as the method is not implemented.</returns>
        public string GetLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing shopping basket record in the database.
        /// </summary>
        /// <param name="model">The ShoppingBasket object containing updated basket details.</param>
        /// <returns>True if the shopping basket was successfully updated; otherwise, false.</returns>
        public bool Update(ShoppingBasket model)
        {
            string sql = $@"Update ShoppingBaskets SET 
                 IsPayed={model.IsPayed.ToString()} WHERE BasketID={model.Id}";
            //string sql = $@"Update ShoppingBaskets SET 
            //     IsPayed=@payed WHERE BasketID=@id";
            //this._dbContext.AddParameter("@payed", Convert.ToString(model.IsPayed));
            //this._dbContext.AddParameter("@id", model.Id);
            return this._dbContext.Update(sql);
        }

        public bool AddProduct(Product product, string basketID, int num)
        {
            string sql = $@"INSERT INTO Product_Basket (ProductID, BasketID, num) VALUES (@pID, @bID, @n);";
            this._dbContext.AddParameter("@pID", product.Id);
            this._dbContext.AddParameter("@bID", basketID);
            this._dbContext.AddParameter("@n", num.ToString());
            return this._dbContext.Update(sql);
        }
        public bool DelProduct(Product product, string basketID)
        {
            string sql = $@"DELETE FROM Product_Basket WHERE ProductID = @pID AND BasketID = @bID;";
            this._dbContext.AddParameter("@pID", product.Id);
            this._dbContext.AddParameter("@bID", basketID);
            return this._dbContext.Update(sql);
        }

        public bool updateAmount(Product product, string basketID, int num)
        {
            string sql = $@"UPDATE Product_Basket SET 
                 num=@num WHERE ProductID = @pID AND BasketID = @bID; ";
            this._dbContext.AddParameter("@pID", product.Id);
            this._dbContext.AddParameter("@bID", basketID);
            this._dbContext.AddParameter("@num", num.ToString());
            return this._dbContext.Update(sql);
        }
    }
}
