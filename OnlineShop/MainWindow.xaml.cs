using Microsoft.Identity.Client.NativeInterop;
using OnlineShop.Models;
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

namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        public MainWindow(Models.Account account)
        {
            InitializeComponent();
            // Now you can access _account's data throughout this window
            DisplayAccountDetails(account);
            loadListview();
        }

        private void DisplayAccountDetails(Models.Account account)
        {
            lbUser.Content = account.Username;
        }

        public void loadListview()
        {
            var listP = context.Products.Select(x => new

            {
                ProductId = x.ProductId,
                Name = x.Name,
                Category = x.Category.Name,
                Price = x.Price,
                QuantityInStock = x.QuantityInStock

            }

        ).ToList().Distinct();

            lvProduct.ItemsSource = listP;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvProduct.SelectedItem as dynamic;
            if (selected != null)
            {
                tbproductId.Text = selected.ProductId.ToString();
                tbproductName.Text = selected.Name.ToString();
                tbcate.Text = selected.Category.ToString();
            }


        }
    }
}