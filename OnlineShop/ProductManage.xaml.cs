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
            LoadDataGrid();
        }

        public void LoadDataGrid()
        {
            var list = context.Products.Select(c => new
            {
                Name = c.Name,
                Price = c.Price,
                Quantity = c.QuantityInStock,
                Catagory = c.Category.Name
            }).Distinct().ToList();
            dGrid.ItemsSource = list;
        }
    }
}
