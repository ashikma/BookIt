using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BookItAdmin.UserControls;

namespace BookItAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool is_login;
        startPage start_Page;
        LoginPage login_Page;
        TreatmentsPage treatmentsPage;

        public MainWindow()
        {
            InitializeComponent();
            //this.is_login = false;
            this.is_login = true;
            setStartWindow();
            ViewStartPage();
        }

        private void ViewStartPage()
        {
            if(this.start_Page == null)
            {
                this.start_Page = new startPage();
            }
            this.brContent.Child = this.start_Page;
        }
        private void ViewLoginPage()
        {
            if(this.login_Page == null)
            {
                this.login_Page = new LoginPage();
            }
            this.brContent.Child = this.login_Page;
        }

        private void setStartWindow()
        {
            if(!this.is_login)
                this.stButtons.Visibility = Visibility.Hidden;
            else
                this.stButtons.Visibility = Visibility.Visible;
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {

            if (!this.is_login)
            {
                ViewLoginPage();
            }
            Button button = (Button)sender;
            if (this.is_login)
            {
                lblogin.Content = "Logout";
                //var uri = new Uri("pack://application:,,,/Resources/logout.jpg");
                //var bitmap = new BitmapImage(uri);
                //imglogin.Source = bitmap;
            }
            else
            {
                lblogin.Content = "Login";
                //var uri = new Uri("pack://application:,,,/Resources/login.jpg");
                //var bitmap = new BitmapImage(uri);
                //imglogin.Source = bitmap;
            }
        }


        private void treatments_Click(object sender, RoutedEventArgs e)
        {
            if(this.treatmentsPage == null)
                this.treatmentsPage = new TreatmentsPage();
            this.brContent.Child = this.treatmentsPage;
        }
    }
}