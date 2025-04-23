using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class TreatmentCreator : IModelCreator<Treatment>
    {
        public Treatment CreateModel(IDataReader src)
        {
            Treatment treatment = new Treatment()
            {
                Id = Convert.ToString(src["TreatmentID"]),
                CostRange = Convert.ToString(src["CostRange"]),
                TimeRange = Convert.ToString(src["TimeRange"]),
                TreatmentName = Convert.ToString(src["TreatmentName"]),
                Explanation = Convert.ToString(src["Explanation"])
            };

            return treatment;
        }
    }
}
