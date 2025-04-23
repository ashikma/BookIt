using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class ShoppingBasketCreator : IModelCreator<ShoppingBasket>
    {
        public ShoppingBasket CreateModel(IDataReader src)
        {
            ShoppingBasket shoppingBasket = new ShoppingBasket()
            {
                Id = Convert.ToString(src["BasketID"]),
                UserID = Convert.ToString(src["UserID"]),
                AddrId = Convert.ToString(src["AddresToDeliver"]),
                CurrLocation = Convert.ToString(src["Location"]),
                IsPayed = Convert.ToBoolean(src["IsPayed"]),
                TotalCost = Convert.ToInt32(src["TotalCost"])
            };
            return shoppingBasket;
        }
    }
}
