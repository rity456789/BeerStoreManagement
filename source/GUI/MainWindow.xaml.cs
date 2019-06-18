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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Product_Clicked(object sender, RoutedEventArgs e)
        {
            Product productWindow = new Product();
            productWindow.ShowDialog();
        }

        private void btnQuit_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            Customer customerWindow = new Customer();
            customerWindow.ShowDialog();
        }

        private void btnSuplier_Clicked(object sender, RoutedEventArgs e)
        {
            Suplier suplierWindow = new Suplier();
            suplierWindow.ShowDialog();
        }

        private void btnImport_Clicked(object sender, RoutedEventArgs e)
        {
            Import importWindow = new Import();
            importWindow.ShowDialog();
        }

        private void btnSell_Clicked(object sender, RoutedEventArgs e)
        {
            Sell sellWindow = new Sell();
            sellWindow.ShowDialog();
        }

        private void btnReport_Clicked(object sender, RoutedEventArgs e)
        {
            Report reportWindow = new Report();
            reportWindow.ShowDialog();
        }
    }
}
