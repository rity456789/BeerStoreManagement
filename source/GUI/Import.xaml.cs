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
using BUS;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Window
    {
        public Import()
        {
            InitializeComponent();
            btnProduct.Content = "";
            btnSuplier.Content = "";
            txtAmount.Text = "";
            lblTotal.Content = "";
            Global.IDProduct = 0;
            Global.IDSuplier = 0;

            DateTime date = DateTime.Now.Date;
            lblDate.Content = date.ToString("MM/dd/yyyy");
        }


        private void btnChooseProduct(object sender, RoutedEventArgs e)
        {
            ProductChoose choose = new ProductChoose();
            choose.ShowDialog();
            btnProduct.Content = Global.IDProduct.ToString();
            if (txtAmount.Text.Count() != 0)
            {
                int temp;
                bool isNumeric = Int32.TryParse(txtAmount.Text, out temp);
                if (isNumeric)
                {
                    UpdatePrice();
                }
            }
        }

        private void btnChooseSuplier(object sender, RoutedEventArgs e)
        {
            SuplierChoose choose = new SuplierChoose();
            choose.ShowDialog();
            btnSuplier.Content = Global.IDSuplier.ToString();
        }

        private void EditAmountChanged(object sender, TextChangedEventArgs e)
        {
            if (Global.IDProduct != 0)
            {
                int temp;
                bool isNumeric = Int32.TryParse(txtAmount.Text, out temp);
                if (isNumeric)
                {
                    UpdatePrice();
                }
            }
        }

        private void UpdatePrice()
        {
            int total = int.Parse(txtAmount.Text);
            ProductBUS bus = new ProductBUS();
            ProductDTO product = bus.LoadOneProduct(Global.IDProduct);
            total = total * product.InputPrice;
            lblTotal.Content = total.ToString();
        }

        private void btnImport_Clicked(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text.Count() == 0 || btnProduct.Content.ToString().Count() == 0 || btnSuplier.Content.ToString().Count() == 0)
            {
                lblNotice.Content = "Please fill all information.";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }


            int amount;
            bool isNumeric = Int32.TryParse(txtAmount.Text, out amount);
            if (!isNumeric)
            {
                lblNotice.Content = "Amount isn't number!";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }



            ProductBUS bus = new ProductBUS();
            ProductDTO result = bus.LoadOneProduct(Global.IDProduct);
            result.IvenNum = result.IvenNum + amount;
            bus.UpdateProduct(result);


            ImportDTO import = new ImportDTO();
            import.Product = Int32.Parse(btnProduct.Content.ToString());
            import.Suplier = Int32.Parse(btnSuplier.Content.ToString());
            import.Amount = amount;
            import.Total = amount * result.InputPrice;
            import.DateInput = lblDate.Content.ToString();

            ImportBUS importBUS = new ImportBUS();
            importBUS.AddNewImport(import);

            this.Close();
        }
    }
}
