﻿using OnlineShop.Models;
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
            loadCategory();
        }

        public void loadProducts()
        {
            var list = context.Products.Select(c => new
            {
                Id = c.ProductId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.QuantityInStock,
                Category = c.Category.Name
            }).Distinct().ToList();
            lstView.ItemsSource = list;
        }

        public void loadCategory()
        {
            var list = context.Products.Select(c => c.Category.Name).Distinct().ToList();
            cbCategory.ItemsSource = list;
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
                tbCategory.Text = selectedProd.Category.ToString();
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
                        Category = c.Category.Name
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

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cbCategory.SelectedItem = null;
            tbId.Text = "";
            tbName.Text = "";
            tbPrice.Text = "";
            tbQuantity.Text = "";
            tbCategory.Text = "";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var Id = Int32.Parse(tbId.Text);
            var prodEdit = context.Products.FirstOrDefault(c => c.ProductId == Id);
            if (prodEdit != null)
            {
                prodEdit.Name = tbName.Text;
                prodEdit.Price = Convert.ToDecimal(tbPrice.Text);
                Category cate = context.Categories.FirstOrDefault(x => x.Name.Equals(tbCategory.Text));
                if(cate == null)
                {
                    Category newCate = new Category();
                    newCate.Name = tbCategory.Text;
                    context.Categories.Add(newCate);
                    prodEdit.Category = newCate;
                }
                else
                {
                    prodEdit.Category = cate;
                }
                prodEdit.QuantityInStock = Convert.ToInt32(tbQuantity.Text);
            }
            context.SaveChanges();
            loadProducts();
            loadCategory();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string Name = tbName.Text;
            string Price = tbPrice.Text;
            string Quantity = tbQuantity.Text;
            Category cate = context.Categories.FirstOrDefault(x => x.Name == tbCategory.Text);
            var addProd = new Product();
            addProd.Name = Name;
            addProd.Price = decimal.Parse(Price);
            addProd.Category = cate;
            addProd.QuantityInStock = Convert.ToInt32(Quantity);
            context.Products.Add(addProd);
            context.SaveChanges();
            loadProducts();
        }
    }
}
