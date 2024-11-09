using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Xml.Linq;

namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for CartManagement.xaml
    /// </summary>
    public partial class CartManagement : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        private Models.Account acc;
        public CartManagement(Models.Account account)
        {
            acc = account;
            InitializeComponent();
            DisplayAccountDetails();
            loadCart();

        }
        private void DisplayAccountDetails()
        {
            accName.Content = acc.Username;
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

        private void lvcartItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvcartItem.SelectedItem as dynamic;
            if (selected != null)
            {
                tbProductId.Text = selected.Id.ToString();
                tbInstock.Text = selected.Instock.ToString();
                tbProductName.Text = selected.Name;
                tbProductQty.Text = selected.Quantity.ToString();
            }
        }

        //refresh
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbProductId.Text = "";
            tbInstock.Text = "";
            tbProductName.Text = "";
            tbProductQty.Text = "";
        }

        //Edit
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbProductId.Text, out var Id) && Id > 0)
            {
                var cartitem = context.CartItems.FirstOrDefault(c => c.ProductId == Id);
                if (cartitem != null)
                {
                    if (int.TryParse(tbProductQty.Text, out var Quantity) && Quantity > 0)
                    {
                        cartitem.ProductQty = Quantity;
                        context.CartItems.Update(cartitem);
                        context.SaveChanges();
                        loadCart();
                    }
                    else
                    {
                        Error.Content = "Please enter a valid quantity (greater than 0).";
                        Error.Foreground = new SolidColorBrush(Colors.Red);
                        Error.Visibility = Visibility.Visible;
                    }

                }
            }


        }

        //delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbProductId.Text, out var Id) && Id > 0)
            {
                var cartitem = context.CartItems.FirstOrDefault(c => c.ProductId == Id);
                context.CartItems.Remove(cartitem);
                context.SaveChanges();
                loadCart();
                tbProductId.Text = "";
                tbInstock.Text = "";
                tbProductName.Text = "";
                tbProductQty.Text = "";
            }
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            checkout pay = new checkout(acc);
            pay.ShowDialog();
            this.Close();
        }
    }
}
