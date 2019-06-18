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
    /// Interaction logic for Suplier.xaml
    /// </summary>
    public partial class Suplier : Window
    {
        public Suplier()
        {
            InitializeComponent();
            LoadSupliers();
        }


        private void LoadSupliers()
        {
            SuplierBUS bus = new SuplierBUS();
            List<SuplierDisplay> list = new List<SuplierDisplay>();
            List<SuplierDTO> supliers = bus.LoadAllSuplier();

            for (int i = 0; i < supliers.Count(); i++)
            {
                list.Add(new SuplierDisplay() { id = supliers[i].ID, name = supliers[i].Name, email = supliers[i].Email });
            }
            listSuplier.ItemsSource = list;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listSuplier.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as SuplierDisplay).name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }




        private void Suplier_selected(object sender, MouseButtonEventArgs e)
        {
            // Lấy dữ liệu item từ list view
            ListViewItem lvi = sender as ListViewItem;
            SuplierDisplay yourDataObject = lvi.DataContext as SuplierDisplay;

            // Nếu như dữ liệu đọc vào ko bị rỗng
            if (yourDataObject != null)
            {
                int id = yourDataObject.id;


                SuplierInfo InfoScreen = new SuplierInfo(id);
                InfoScreen.ShowDialog();
                LoadSupliers();
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listSuplier.ItemsSource).Refresh();
        }

        private void btnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            SuplierAdd AddScreen = new SuplierAdd();
            AddScreen.ShowDialog();
            LoadSupliers();
        }
    }
}
