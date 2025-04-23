using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt
{
    public class Appointment : Model
    {
        public string ManagerID { get; set; }
        public int CostumerId {  get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Oclock { get; set; }
        public int TreatmentId { get; set; }
        public int Cost { get; set; }
    }
}
