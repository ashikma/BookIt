using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class AppointmentCreator : IModelCreator<Appointment>
    {
        public Appointment CreateModel(IDataReader src)
        {
            Appointment appointment = new Appointment()
            {
                Id = Convert.ToString(src["ApointmentID"]),
                CostumerId = Convert.ToInt32(src["UserID"]),
                Date = Convert.ToString(src["Date"]),
                Time = Convert.ToString(src["Time"]),
                Oclock = Convert.ToString(src["Oclock"]),
                Cost = Convert.ToInt32(src["Cost"]),
                TreatmentId = Convert.ToInt32(src["TreatmentId"])
            };
            return appointment;
        }
    }
}
