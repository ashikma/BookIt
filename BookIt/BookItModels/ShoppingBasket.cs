using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt
{
    public class ShoppingBasket : Model
    {
        public string UserID { get; set; }
        public int TotalCost { get; set; }
        public string AddrId { get; set; }
        public string CurrLocation { get; set; }
        public bool IsPayed { get; set; }
    }
}
