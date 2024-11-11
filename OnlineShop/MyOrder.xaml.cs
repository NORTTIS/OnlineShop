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
    /// Interaction logic for MyOrder.xaml
    /// </summary>
    public partial class MyOrder : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        Models.Account acc;
        public MyOrder(Account account)
        {
            InitializeComponent();
            acc = account;
            DisplayAccountDetails();
            loadOrder();

        }
        private void DisplayAccountDetails()
        {
            accName.Content = acc.Username;
        }
        private void loadOrder()
        {

            var listOrder = context.Orders.Where(o => o.AccountId == acc.AccountId)
                .Select(x => new
                {
                    OrderId = x.OrderId,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderDate = x.OrderDate,

                })
                .ToList().Distinct();

            lvOrder.ItemsSource = listOrder;

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


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvOrder.SelectedItem as dynamic;
            if (selected != null)
            {
                int orderId = (int)selected.OrderId; ;
                var carItem = context.OrderItems.Where(c => c.OrderId == orderId)
                    .Select(x => new
                    {
                        Id = x.ProductId,
                        Instock = x.Product.QuantityInStock,
                        Name = x.Product.Name,
                        Quantity = x.ProdQty,
                        Price = x.TotalPrice
                    })
                    .ToList().Distinct();
                lvOrderCart.ItemsSource = carItem;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CartManagement cartManagement = new CartManagement(acc);
            cartManagement.ShowDialog();
            this.Close();
        }
    }
}
