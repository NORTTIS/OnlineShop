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
    /// Interaction logic for ProductManage.xaml
    /// </summary>
    public partial class ProductManage : Window
    {
        OnlineShopContext context = new OnlineShopContext();
        public ProductManage()
        {
            InitializeComponent();
            loadProducts();
        }

        public void loadProducts()
        {
            var list = context.Products.Select(c => new
            {
                Name = c.Name,
                Price = c.Price,
                Quantity = c.QuantityInStock,
                Category = c.Category.Name
            }).Distinct().ToList();
            lstView.ItemsSource = list;
        }

        private void lbDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProd = lstView.SelectedItem as dynamic;
            if (selectedProd != null)
            {
                tbName.Text = selectedProd.Name.ToString();
            }
        }

    }
}
