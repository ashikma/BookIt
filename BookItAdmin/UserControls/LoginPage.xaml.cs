using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

using BookItModels;
using WebApiClient;

namespace BookItAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        TreatmentsPage treatmentsPage;
        public LoginPage()
        {
            InitializeComponent();
        }

        public string userId;

        public string UserId { get { return this.userId; } }

        private async void btLog_Click(object sender, RoutedEventArgs e)
        {
            WebClient<string> webClient = new WebClient<string>();
            webClient.Schema = "http";
            webClient.Port = 5221;
            webClient.Host = "localhost";
            webClient.Path = "api/User/SingIn";
            webClient.AddParam("username", this.UsernameBox.Text);
            webClient.AddParam("password", this.PasswordBox.Password.ToString());
            
            string userId = await webClient.Get();
            if (userId == "" || userId == null)
            {
                //ViewTreatmentsPage();
                return;
            }
            else this.userId = userId;
        }


        
    }
}
