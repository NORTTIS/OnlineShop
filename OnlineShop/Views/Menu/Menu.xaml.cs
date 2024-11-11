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

namespace OnlineShop.Views.Menu
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            if (App.UserRole.Equals("Admin"))
            {
                var adminMenu = new AdminMenu();
                adminMenu.Show();
            }
            else if (App.UserRole.Equals("Manager"))
            {
                var managerMenu = new ManagerMenu();
                managerMenu.Show();
            }
            else
            {
                var userMenu = new UserMenu();
                userMenu.Show();
            }
            this.Close();
        }
    }
}
