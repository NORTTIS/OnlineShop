using Microsoft.IdentityModel.Tokens;
using OnlineShop.DTOs;
using OnlineShop.Models;
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

namespace OnlineShop.Views
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private readonly OnlineShopContext dbContext = new OnlineShopContext();
        private Window owner;

        public UserManagement(Window owner)
        {
            InitializeComponent();
            LoadAccounts();
            this.owner = owner;
        }

        private void LoadAccounts()
        {
            var accounts = dbContext.Account.ToList();
            List<AccountDTO> results = new List<AccountDTO>();
            foreach (var account in accounts)
            {
                AccountDTO accountDTO = new AccountDTO();
                accountDTO.AccountId = account.AccountId;
                accountDTO.Username = account.Username;
                accountDTO.Address = account.Address;
                accountDTO.Email = account.Email;
                accountDTO.PhoneNumber = account.PhoneNumber;
                accountDTO.Role = account.Role;
                results.Add(accountDTO);
            }

            UserDataGrid.ItemsSource = results;
        }

        private void UserManagement_Closing(object sender, CancelEventArgs e)
        {
            owner.Visibility = Visibility.Visible;
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = UserDataGrid.SelectedItem as AccountDTO;

            if (selectedUser != null)
            {
                IdTextBox.Text = selectedUser.AccountId.ToString();
                UsernameTextBox.Text = selectedUser.Username;
                UsernameTextBox.IsEnabled = false;
                PasswordBox.IsEnabled = false;
                AddressTextBox.Text = selectedUser.Address;
                EmailTextBox.Text = selectedUser.Email;
                EmailTextBox.IsEnabled = false;
                PhoneNumberTextBox.Text = selectedUser.PhoneNumber;
                foreach (ComboBoxItem item in RoleComboBox.Items)
                {
                    if (item.Content.ToString() == selectedUser.Role)
                    {
                        RoleComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                IdTextBox.Text = string.Empty;
                UsernameTextBox.Text = string.Empty;
                UsernameTextBox.IsEnabled = true;
                PasswordBox.IsEnabled = true;
                AddressTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                EmailTextBox.IsEnabled = true;
                PhoneNumberTextBox.Text = string.Empty;
                foreach (ComboBoxItem item in RoleComboBox.Items)
                {
                    if (item.Content.ToString() == "Customer")
                    {
                        RoleComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            // Handle required fields
            if (username.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter username!");
                return;
            }
            if (email.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter email");
                return;
            }

            // Handle add
            if (id.Trim().IsNullOrEmpty())
            {
                if (password.IsNullOrEmpty())
                {
                    MessageBox.Show("Please enter password");
                    return;
                }
                if (dbContext.Account.Any(
                    u => (u.Username.Equals(username.Trim()))))
                {
                    MessageBox.Show("Username " + username + " has been taken, please choose another username!");
                    return;
                }
                if (dbContext.Account.Any(
                    u => (u.Email.Equals(email.Trim()))))
                {
                    MessageBox.Show("This email has been taken, please provide another email!");
                    return;
                }

                Account account = new Account(username, password, address, email, phoneNumber, role);
                dbContext.Account.Add(account);
                dbContext.SaveChanges();
                MessageBox.Show("New user created successfully!");
            }
            // Handle update
            else
            {
                Account account = dbContext.Account.FirstOrDefault(u => u.AccountId.Equals(Int32.Parse(id)));
                account.Address = address;
                account.PhoneNumber = phoneNumber;
                account.Role = role;
                dbContext.SaveChanges();
                MessageBox.Show("User updated successfully!");
            }
            LoadAccounts();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = IdTextBox.Text;
            if (id.IsNullOrEmpty())
            {
                MessageBox.Show("Please select a user to delete!");
                return;
            }
            Account? account = dbContext.Account.FirstOrDefault(u => (u.AccountId.Equals(Int32.Parse(id))));
            if (account.Username.ToLower().Equals("admin"))
            {
                MessageBox.Show("This user cannot be deleted.");
                return;
            }
            dbContext.Account.Remove(account);
            dbContext.SaveChanges();
            MessageBox.Show("User deleted successfully!");
            LoadAccounts();
        }
    }
}
