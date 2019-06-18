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
using DTO;
using BUS;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public Customer()
        {
            InitializeComponent();
            LoadCustomers();
        }


        private void LoadCustomers()
        {
            CustomerBUS bus = new CustomerBUS();
            List<CustomerDisplay> list = new List<CustomerDisplay>();
            List<CustomerDTO> customers = bus.LoadAllCustomer();

            for (int i = 0; i < customers.Count(); i++)
            {
                list.Add(new CustomerDisplay() { id = customers[i].ID, name = customers[i].Name, phone = customers[i].Phone });
            }
            listCustomer.ItemsSource = list;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listCustomer.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as CustomerDisplay).name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }




        private void Customer_selected(object sender, MouseButtonEventArgs e)
        {
            // Lấy dữ liệu item từ list view
            ListViewItem lvi = sender as ListViewItem;
            CustomerDisplay yourDataObject = lvi.DataContext as CustomerDisplay;

            // Nếu như dữ liệu đọc vào ko bị rỗng
            if (yourDataObject != null)
            {
                int id = yourDataObject.id;


                CustomerInfo InfoScreen = new CustomerInfo(id);
                InfoScreen.ShowDialog();
                LoadCustomers();
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listCustomer.ItemsSource).Refresh();
        }

        private void btnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            CustomerAdd AddScreen = new CustomerAdd();
            AddScreen.ShowDialog();
            LoadCustomers();
        }
    }
}
