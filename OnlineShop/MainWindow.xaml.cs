using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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
        private Models.Account acc;
        public MainWindow(Models.Account account)
        {
            InitializeComponent();
            // Now you can access _account's data throughout this window
            acc = account;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbProductQty.Text, out var productId) && productId > 0)
            {
                var accountId = acc.AccountId;
                var errorLabel = lbError;
                // Validate quantity
                if (!int.TryParse(tbProductQty.Text, out var quantity) || quantity <= 0)
                {
                    errorLabel.Content = "Please enter a valid quantity (greater than 0).";
                    errorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    errorLabel.Visibility = Visibility.Visible;
                    return;
                }

                errorLabel.Visibility = Visibility.Hidden;

                int cartId = GetOrCreateCart(accountId);

                AddItemToCart(cartId, productId, quantity);
            }

        }

        //get/create cart
        private int GetOrCreateCart(int accountId)
        {
            var cart = context.Carts.FirstOrDefault(c => c.AccountId == accountId);

            if (cart != null)
            {
                // If the cart exists, return the cartId
                return cart.CartId;
            }
            else
            {
                // If the cart doesn't exist, create a new cart and save it
                var newCart = new Cart
                {
                    AccountId = accountId,
                    CreateAt = DateTime.Now // Assuming you have this property to store creation date
                };

                context.Carts.Add(newCart);
                context.SaveChanges();

                // Return the ID of the newly created cart
                return newCart.CartId;
            }

        }

        // Add the item to the cart
        private void AddItemToCart(int cartId, int productId, int quantity)
        {
            var cart = context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cart != null)
            {
                cart.ProductQty += quantity;
                context.CartItems.Update(cart);

            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    ProductQty = quantity
                };

                context.CartItems.Add(cartItem);
            }


            context.SaveChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CartManagement cartManagement = new CartManagement(acc);
            cartManagement.ShowDialog();
            this.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MyOrder myOrder = new MyOrder(acc);
            myOrder.ShowDialog();
            this.Close();

        }
    }
}