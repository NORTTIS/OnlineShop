using Microsoft.Identity.Client.NativeInterop;
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

namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Signup signup = new Signup();
            signup.ShowDialog();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = tbusername.Text.ToString();
            string password = tbpassword.Text.ToString();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Models.Account acc = context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password);
                if (acc != null && acc.Role.Equals("Customer"))
                {
                    MainWindow window = new MainWindow(acc); // Pass the account to the new window
                    this.Hide();
                    window.ShowDialog();
                    this.Close();
                }
                else if (acc != null && acc.Role.Equals("Manager"))
                {
                    ProductManage window = new ProductManage(); // Pass the account to the new window
                    this.Hide();
                    window.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }

        }

    }
}
