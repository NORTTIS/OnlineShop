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
                        Quantity = x.ProductQty,
                        Price = x.Product.Price

                    }).ToList().Distinct();
                lvcartItem.ItemsSource = listCart;
            }

        }

        public void loadUserInfo(Models.Account accInfo)
        {
            tbFullname.Text = accInfo.Username;
            accName.Content = accInfo.Username;
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

        //checkout
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                var cart = context.Carts.FirstOrDefault(c => c.AccountId == acc.AccountId);
                var listCartOrder = context.CartItems
                    .Where(c => c.CartId == cart.CartId)
                    .Select(x => new
                    {
                        Id = x.ProductId,
                        Instock = x.Product.QuantityInStock,
                        Name = x.Product.Name,
                        Quantity = x.ProductQty,
                        Price = x.Product.Price

                    }).ToList().Distinct();
                if (cart != null)
                {
                    Order order = new Order();
                    order.Status = "Pending";
                    order.AccountId = acc.AccountId;
                    order.OrderDate = DateTime.Now;
                    var total = 0;
                    foreach (var item in listCartOrder)
                    {
                        total += (int)item.Price;
                    }

                    order.TotalAmount = total;
                    context.Orders.Add(order);
                    context.SaveChanges();  // Lưu thay đổi để tạo orderId
                    int orderId = order.OrderId;
                    bool isValidOrder = true;
                    foreach (var item in listCartOrder)
                    {
                        var prod = context.Products.FirstOrDefault(x => x.ProductId == item.Id);
                        int resultQty = prod.QuantityInStock - (int)item.Quantity;
                        if (resultQty < 0)
                        {
                            isValidOrder = false;
                            Mes.Content = "Order not valid, some product out of stock. ";
                            Mes.Foreground = new SolidColorBrush(Colors.Red);
                            Mes.Visibility = Visibility.Visible;
                            return;
                        }
                    }
                    foreach (var item in listCartOrder)
                    {
                        var orderItem = new OrderItem();
                        orderItem.OrderId = orderId;
                        orderItem.ProductId = item.Id;
                        orderItem.ProdQty = (int)item.Quantity;
                        orderItem.TotalPrice = order.TotalAmount;
                        var prod = context.Products.FirstOrDefault(x => x.ProductId == item.Id);
                        prod.QuantityInStock -= (int)item.Quantity;
                        context.Products.Update(prod);
                        context.OrderItems.Add(orderItem);
                        context.SaveChanges();
                    }

                }
                Mes.Content = "Order successful! ";
                Mes.Foreground = new SolidColorBrush(Colors.Green);
                Mes.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                Mes.Content = e.ToString();
                Mes.Foreground = new SolidColorBrush(Colors.Red);
                Mes.Visibility = Visibility.Visible;
            }

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
                    Mes.Content = "Email wrong - format.";
                    Mes.Foreground = new SolidColorBrush(Colors.Red);
                    Mes.Visibility = Visibility.Visible;
                    return;
                }
                if (accByEmail != null && (!accByEmail.AccountId.Equals(acc.AccountId)))
                {
                    Mes.Content = "Email already existed";
                    Mes.Foreground = new SolidColorBrush(Colors.Red);
                    Mes.Visibility = Visibility.Visible;
                    return;
                }
                if (!ValidateVietnamesePhoneNumber(phone))
                {
                    Mes.Content = "phone number - wrong format.";
                    Mes.Foreground = new SolidColorBrush(Colors.Red);
                    Mes.Visibility = Visibility.Visible;
                    return;
                }
                if (!isValidForm)
                {
                    Mes.Content = "Please enter a valid value.";
                    Mes.Foreground = new SolidColorBrush(Colors.Red);
                    Mes.Visibility = Visibility.Visible;
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
                    Mes.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                Mes.Content = e.ToString();
                Mes.Foreground = new SolidColorBrush(Colors.Red);
                Mes.Visibility = Visibility.Visible;
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
