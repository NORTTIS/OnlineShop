using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Net;


namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for checkout.xaml
    /// </summary>
    public partial class checkout : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        private Models.Account acc;
        public checkout(Models.Account account)
        {
            InitializeComponent();
            acc = account;
            loadCart();
            loadUserInfo(acc);
        }
        public void loadCart()
        {
            var cartId = context.Carts.FirstOrDefault(c => c.AccountId == acc.AccountId);
            if (cartId != null)
            {
                var listCart = context.CartItems.Where(c => c.CartId == cartId.CartId)
                    .Select(x => new
                    {
                        Id = x.ProductId,
                        Instock = x.Product.QuantityInStock,
                        Name = x.Product.Name,
                        Quantity = x.ProductQty

                    }).ToList().Distinct();
                lvcartItem.ItemsSource = listCart;
            }

        }

        public void loadUserInfo(Models.Account accInfo)
        {
            tbFullname.Text = accInfo.Username;
            tbEmail.Text = accInfo.Email;
            tbPhone.Text = accInfo.PhoneNumber;
            tbAddress.Text = accInfo.Address;

        }

        //home
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow(acc);
            mainWindow.ShowDialog();
            this.Close();
        }

        //log out
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CartManagement login = new CartManagement(acc);
            login.ShowDialog();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var fullName = tbFullname.Text;
            var email = tbEmail.Text;
            var phone = tbPhone.Text;
            var address = tbAddress.Text;
            bool isValidForm = (!String.IsNullOrEmpty(email)) && (!String.IsNullOrEmpty(phone)) && (!String.IsNullOrEmpty(address));

            var accByEmail = context.Accounts.FirstOrDefault(a => a.Email == email);

            try
            {

                if (!ValidateEmail(email))
                {
                    Error.Content = "Email wrong - format.";
                    Error.Foreground = new SolidColorBrush(Colors.Red);
                    Error.Visibility = Visibility.Visible;
                    return;
                }
                if (accByEmail != null && (!accByEmail.AccountId.Equals(acc.AccountId)))
                {
                    Error.Content = "Email already existed";
                    Error.Foreground = new SolidColorBrush(Colors.Red);
                    Error.Visibility = Visibility.Visible;
                    return;
                }
                if (!ValidateVietnamesePhoneNumber(phone))
                {
                    Error.Content = "phone number - wrong format.";
                    Error.Foreground = new SolidColorBrush(Colors.Red);
                    Error.Visibility = Visibility.Visible;
                    return;
                }
                if (!isValidForm)
                {
                    Error.Content = "Please enter a valid value.";
                    Error.Foreground = new SolidColorBrush(Colors.Red);
                    Error.Visibility = Visibility.Visible;
                    return;
                }



                if (isValidForm)
                {
                    if (accByEmail == null)
                    {
                        acc.Email = email;
                    }
                    acc.PhoneNumber = phone;
                    acc.Address = address;
                    context.Accounts.Update(acc);
                    loadUserInfo(acc);
                    context.SaveChanges();
                    Error.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                Error.Content = e.ToString();
                Error.Foreground = new SolidColorBrush(Colors.Red);
                Error.Visibility = Visibility.Visible;
            }






        }
        public bool ValidateVietnamesePhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^(03|05|07|08|09)[0-9]{8}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        public bool ValidateEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }


    }
}
