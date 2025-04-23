using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class ProductCreator : IModelCreator<Product>
    {
        public Product CreateModel(IDataReader src)
        {
            Product product = new Product()
            {
                Id = Convert.ToString(src["ProductID"]),
                Cost = Convert.ToInt32(src["Cost"]),
                Description = Convert.ToString(src["Description"]),
                NumInStock = Convert.ToInt32(src["NumInStock"]),
                ProductName = Convert.ToString(src["ProductName"])
            };
            return product;
        }
    }
}
