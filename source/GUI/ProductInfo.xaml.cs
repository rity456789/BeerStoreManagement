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
    /// Interaction logic for ProductInfo.xaml
    /// </summary>
    public partial class ProductInfo : Window
    {
        int ID;
        public ProductInfo(int id)
        {
            InitializeComponent();
            LoadInfo(id);
            ID = id;
        }

        private void LoadInfo(int id)
        {
            ProductBUS bus = new ProductBUS();
            ProductDTO result = bus.LoadOneProduct(id);
            txtID.Content = result.ID.ToString();
            txtName.Text = result.Name;
            lblAmount.Content = result.IvenNum.ToString();
            txtInputPrice.Text = result.InputPrice.ToString();
            txtOutputPrice.Text = result.OutputPrice.ToString();
        }

        private void btnSave_Clicked(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Count() == 0 || txtInputPrice.Text.Count() == 0 || txtOutputPrice.Text.Count() == 0)
            {
                lblNotice.Content = "Please fill all information.";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }


            int temp;
            bool isNumeric = Int32.TryParse(txtInputPrice.Text, out temp);
            if (!isNumeric)
            {
                lblNotice.Content = "Input price isn't number!";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }

            isNumeric = Int32.TryParse(txtOutputPrice.Text, out temp);
            if (!isNumeric)
            {
                lblNotice.Content = "Output price isn't number!";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }

            ProductBUS bus = new ProductBUS();
            ProductDTO result = new ProductDTO();
            result.ID = ID;
            result.Name = txtName.Text;
            result.IvenNum = Int32.Parse(lblAmount.Content.ToString());
            result.InputPrice = Int32.Parse(txtInputPrice.Text);
            result.OutputPrice = Int32.Parse(txtOutputPrice.Text);
            bus.UpdateProduct(result);
            this.Close();
        }

        private void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            ProductBUS bus = new ProductBUS();
            bus.DeleteProduct(ID);
            this.Close();
        }
    }
}
