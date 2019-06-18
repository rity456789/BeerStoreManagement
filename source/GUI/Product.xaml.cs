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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product()
        {
            InitializeComponent();
            LoadProducts();
        }


        private void LoadProducts()
        {
            ProductBUS bus = new ProductBUS();
            List<ProductDisplay> list = new List<ProductDisplay>();
            List<ProductDTO> products = bus.LoadAllProduct();

            for (int i = 0; i < products.Count(); i++)
            {
                list.Add(new ProductDisplay() { id = products[i].ID, name = products[i].Name, inputprice = products[i].InputPrice, outputprice = products[i].OutputPrice });
            }
            listProduct.ItemsSource = list;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listProduct.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as ProductDisplay).name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }




        private void Product_selected(object sender, MouseButtonEventArgs e)
        {
            // Lấy dữ liệu item từ list view
            ListViewItem lvi = sender as ListViewItem;
            ProductDisplay yourDataObject = lvi.DataContext as ProductDisplay;

            // Nếu như dữ liệu đọc vào ko bị rỗng
            if (yourDataObject != null)
            {
                int id = yourDataObject.id;


                ProductInfo InfoScreen = new ProductInfo(id);
                InfoScreen.ShowDialog();
                LoadProducts();
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listProduct.ItemsSource).Refresh();
        }

        private void btnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            ProductAdd AddScreen = new ProductAdd();
            AddScreen.ShowDialog();
            LoadProducts();
        }
    }
}
