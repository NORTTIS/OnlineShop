using OnlineShop.Models;
using OnlineShop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OnlineShop.Views.Menu
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void UserManagementButton_Click(object sender, RoutedEventArgs e)
        {
            var userManagementWindow = new UserManagement(this);
            userManagementWindow.Show();
            this.Hide();
        }

        private void CategoryManagementButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ProductManagementButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OrderManagementButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            this.Close();
        }

        private void AdminMenu_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }
    }
}
