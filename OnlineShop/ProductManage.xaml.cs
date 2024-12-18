﻿using ClosedXML.Excel;
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
using Microsoft.Win32;
using Microsoft.IdentityModel.Tokens;

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
            loadCategory();
        }

        public void loadProducts()
        {

            var list = context.Products.Where(c => c.Status == "Available").Select(c => new

            {
                Id = c.ProductId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.QuantityInStock,
                Category = c.Category.Name,
                Status = c.Status
            }).Distinct().ToList();
            lstView.ItemsSource = list;
        }

        public void loadCategory()
        {
            var list = context.Products.Select(c => c.Category.Name).Distinct().ToList();
            cbCategory.ItemsSource = list;
            cbCate.ItemsSource = list;
        }

        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProd = lstView.SelectedItem as dynamic;
            if (selectedProd != null)
            {
                tbId.Text = selectedProd.Id.ToString();
                tbName.Text = selectedProd.Name.ToString();
                tbPrice.Text = selectedProd.Price.ToString();
                tbQuantity.Text = selectedProd.Quantity.ToString();
                cbCate.Text = selectedProd.Category.ToString();
            }
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategory.SelectedItem != null)
            {
                var selectedCategory = cbCategory.SelectedItem.ToString();
                if (selectedCategory != null)
                {
                    var list = context.Products.Where(x => x.Category.Name.Equals(selectedCategory)).Select(c => new
                    {
                        Id = c.ProductId,
                        Name = c.Name,
                        Price = c.Price,
                        Quantity = c.QuantityInStock,
                        Category = c.Category.Name,
                        Status = c.Status
                    }).ToList();
                    lstView.ItemsSource = list;
                }
            }
            else
            {
                loadProducts();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var Id = Int32.Parse(tbId.Text);
            var prodDel = context.Products.FirstOrDefault(c => c.ProductId == Id);
            if (prodDel != null)
            {
                prodDel.Status = "Unavailable";
            }
            context.SaveChanges();
            loadProducts();
            loadCategory();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cbCategory.SelectedItem = null;
            tbId.Text = "";
            tbName.Text = "";
            tbPrice.Text = "";
            tbQuantity.Text = "";
            cbCate.SelectedItem = null;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbPrice.Text) ||
                string.IsNullOrWhiteSpace(tbQuantity.Text) ||
                Convert.ToInt32(tbQuantity.Text) <= 0)
            {
                return;
            }
            var Id = Int32.Parse(tbId.Text);
            var prodEdit = context.Products.FirstOrDefault(c => c.ProductId == Id);
            if (prodEdit != null)
            {
                prodEdit.Name = tbName.Text;
                prodEdit.Price = Convert.ToDecimal(tbPrice.Text);
                Category cate = context.Categories.FirstOrDefault(x => x.Name.Equals(cbCate.Text));
                prodEdit.Category = cate;
                prodEdit.QuantityInStock = Convert.ToInt32(tbQuantity.Text);
                context.SaveChanges();
                loadProducts();
                loadCategory();
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbPrice.Text) ||
                string.IsNullOrWhiteSpace(tbQuantity.Text) ||
                Convert.ToInt32(tbQuantity.Text) <= 0)
            {
                return;
            }
            string Name = tbName.Text;
            string Price = tbPrice.Text;
            string Quantity = tbQuantity.Text;
            Category cate = context.Categories.FirstOrDefault(x => x.Name == cbCate.Text);
            var addProd = new Product();
            addProd.Name = Name;
            addProd.Price = decimal.Parse(Price);
            addProd.Category = cate;
            addProd.QuantityInStock = Convert.ToInt32(Quantity);
            addProd.Status = "Available";
            context.Products.Add(addProd);
            context.SaveChanges();
            loadProducts();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                Title = "Products file"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Name";
                    worksheet.Cell(1, 3).Value = "Price";
                    worksheet.Cell(1, 4).Value = "Quantity In Stock";
                    worksheet.Cell(1, 5).Value = "Category";
                    var items = lstView.Items as dynamic;
                    for (int i = 0; i < lstView.Items.Count; i++)
                    {
                        var item = items[i];
                        worksheet.Cell(i + 2, 1).Value = item.Id;
                        worksheet.Cell(i + 2, 2).Value = item.Name;
                        worksheet.Cell(i + 2, 3).Value = item.Price;
                        worksheet.Cell(i + 2, 4).Value = item.Quantity;
                        worksheet.Cell(i + 2, 5).Value = item.Category;
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Exported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}
