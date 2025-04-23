using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt
{
    public class Product : Model
    {
        public int Cost { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int NumInStock { get; set; }
    }
}
