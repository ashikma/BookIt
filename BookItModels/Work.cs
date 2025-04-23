using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt
{
    public class Work : Model
    {
        public int Cost { get; set; }
        public string ImgName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public int treatmentID { get; set; }
    }
}
