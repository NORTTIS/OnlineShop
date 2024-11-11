using Microsoft.IdentityModel.Tokens;
using OnlineShop.Models;
using OnlineShop.Views;
using OnlineShop.Views.Menu;
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
using Menu = OnlineShop.Views.Menu.Menu;

namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OnlineShopContext dbContext = new OnlineShopContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username.Trim().IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter username and password!");
                return;
            }

            Account? user = dbContext.Account.FirstOrDefault(u => (u.Username.Equals(username.Trim())
                && u.Password.Equals(password)));
            if (user == null || user == default)
            {
                MessageBox.Show("Username or password is incorrect.");
                return;
            }

            App.UserRole = user.Role;
            App.UserId = user.AccountId;

            var menu = new Menu();
            this.Hide();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}