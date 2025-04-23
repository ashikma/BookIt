using BookIt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApiClient;

namespace BookItAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for TreatmentDetails.xaml
    /// </summary>
    public partial class TreatmentDetails : Window
    {
        Treatment treatment;
        public TreatmentDetails()
        {
            InitializeComponent();
        }
        public TreatmentDetails(string treatmentID)
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void GetTreatment(string id)
        {
            WebClient<Treatment> webClient = new WebClient<Treatment>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/GetTreatments";
            webClient.AddParam("tId", id);
            this.treatment = await webClient.Get();
            this.DataContext = this.treatment;
        }
    }
}
