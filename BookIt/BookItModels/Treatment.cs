using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt
{
    public class Treatment : Model
    {
        public string TreatmentName {  get; set; }
        public string CostRange { get; set; }
        public string TimeRange { get; set; }
        public string Explanation {  get; set; }
    }
}
