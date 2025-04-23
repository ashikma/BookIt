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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApiClient;

namespace BookItAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for treatments.xaml
    /// </summary>
    public partial class TreatmentsPage : UserControl
    {
        List<Treatment> _treatments;
        public TreatmentsPage()
        {
            InitializeComponent();
            GetAllTreatments();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async Task GetAllTreatments()
        {
            WebClient<List<Treatment>> webClient = new WebClient<List<Treatment>>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/Guest/GetTreatments";
            this._treatments = await webClient.Get();
            this.listView.ItemsSource = this._treatments;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string id = b.Tag.ToString();
            TreatmentDetails details = new TreatmentDetails();
            details.ShowDialog();
        }
    }
}
