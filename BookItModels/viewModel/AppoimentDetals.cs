using BookIt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookItModels.viewModel
{
    public class AppoimentDetals
    {
       public Treatment Treatment { get; set; }
        public string Cost { get; set; }
        public string Date { get; set; }
        public string Oclock { get; set; }
    }
}
