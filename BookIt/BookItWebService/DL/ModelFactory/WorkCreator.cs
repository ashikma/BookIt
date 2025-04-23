using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class WorkCreator : IModelCreator<Work>
    {
        public Work CreateModel(IDataReader src)
        {
            //present the time in h,m
            int time = Convert.ToInt32(src["Time"]);
            int hours = time/60;
            int minutes = time%60;
            string t = "";
            if (hours > 0)
                t += hours.ToString() + "h ";
            if (minutes > 0)
                t += minutes.ToString() + "m";

            Work work = new Work()
            {
                Id = Convert.ToString(src["WorkID"]),
                ImgName = "http://localhost:5221/img/" + Convert.ToString(src["ImgName"]),
                Cost = Convert.ToInt32(src["Cost"]),
                Date = Convert.ToString(src["Date"]),
                Time = t
            };
            return work;
        }
    }
}
