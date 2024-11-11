using Microsoft.Identity.Client.NativeInterop;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Models;
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

namespace OnlineShop.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly OnlineShopContext dbContext = new OnlineShopContext();

        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var account = dbContext.Account.FirstOrDefault(u => u.AccountId.Equals(App.UserId));

            var currentPassword = CurrentPasswordBox.Password;
            var newPassword = NewPasswordBox.Password;
            var confirmPassword = ConfirmNewPasswordBox.Password;

            if (currentPassword.IsNullOrEmpty() ||
                newPassword.IsNullOrEmpty() ||
                confirmPassword.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter all fields!");
                return;
            }

            if (currentPassword != account.Password)
            {
                MessageBox.Show("Wrong current password");
                CurrentPasswordBox.Focus();
                return;
            }

            if (currentPassword.Equals(newPassword))
            {
                MessageBox.Show("New password can not be the same with current password.");
                NewPasswordBox.Focus();
                return;
            }

            if (!confirmPassword.Equals(newPassword))
            {
                MessageBox.Show("Confirm password does not match.");
                return;
            }

            account.Password = newPassword;
            dbContext.SaveChanges();
            MessageBox.Show("Password changed successfully!");
            this.Close();
        }
    }
}
