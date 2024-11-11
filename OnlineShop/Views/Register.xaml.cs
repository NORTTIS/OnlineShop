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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly OnlineShopContext dbContext = new OnlineShopContext();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle username
            string username = UsernameTextBox.Text.Trim();
            if (username.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter username!");
                UsernameTextBox.Focus();
                return;
            }
            if (dbContext.Account.Any(u => u.Username == username))
            {
                MessageBox.Show("This username has been taken, please choose another username!");
                UsernameTextBox.Focus();
                return;
            }

            // Handle password
            string password = PasswordBox.Password.Trim();
            string confirmPassword = ConfirmPasswordBox.Password.Trim();
            if (password.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter password!");
                PasswordBox.Focus();
                return;
            }
            if (confirmPassword.IsNullOrEmpty())
            {
                MessageBox.Show("Please re-enter password to confirm!");
                ConfirmPasswordBox.Focus();
                return;
            }
            if (!password.Equals(confirmPassword))
            {
                MessageBox.Show("Passwords do not match, please re-enter!");
                ConfirmPasswordBox.Focus();
                return;
            }

            string address = AddressTextBox.Text.Trim();

            // Handle email
            string email = EmailTextBox.Text.Trim();
            if (email.Length == 0)
                email = null;

            if (email.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter email!");
                return;
            }
            if (dbContext.Account.Any(u => u.Email == email))
            {
                MessageBox.Show("This email already existed, please enter another email!");
                return;
            }

            string phoneNumber = PhoneNumberTextBox.Text.Trim();
            if (phoneNumber.Length == 0)
                phoneNumber = null;

            string role = "Customer";

            dbContext.Account.Add(new Account(username, password, address, email, phoneNumber, role));
            dbContext.SaveChanges();
            MessageBox.Show("Registered successfully!");
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
