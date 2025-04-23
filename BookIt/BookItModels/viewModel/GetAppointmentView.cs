using BookIt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookItModels.viewModel
{
    public class GetAppointmentView
    {
        public List<User> Managers { get; set; }

        public List<string> optionalAppoinments { get; set; }
    }
}
