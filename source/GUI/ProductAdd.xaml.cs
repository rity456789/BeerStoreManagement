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
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        public ProductAdd()
        {
            InitializeComponent();
            lblAmount.Content = 0;
        }

        private void btnAdd_clicked(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Count() == 0  || txtInputPrice.Text.Count() == 0 || txtOutputPrice.Text.Count() == 0 )
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
            result.Name = txtName.Text;
            result.IvenNum = 0;
            result.InputPrice = Int32.Parse(txtInputPrice.Text);
            result.OutputPrice = Int32.Parse(txtOutputPrice.Text);
            bus.AddNewProduct(result);
            this.Close();
        }
    }
}
