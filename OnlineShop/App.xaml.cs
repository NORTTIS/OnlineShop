using System.Configuration;
using System.Data;
using System.Windows;

namespace OnlineShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static String UserRole { get; set; }
        public static int UserId { get; set; }
    }

}
