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
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell : Window
    {
        int discount = 0;
        public Sell()
        {
            InitializeComponent();
            btnProduct.Content = "";
            btnCustomer.Content = "";
            txtAmount.Text = "";
            lblTotal.Content = "";
            Global.IDProduct = 0;
            Global.IDCustomer = 0;

            DateTime date = DateTime.Now.Date;
            lblDate.Content = date.ToString("MM/dd/yyyy");
            LoadDiscount();
        }

        private void LoadDiscount()
        {
            if (Global.IDCustomer == 0)
            {
                btnDiscount.Content = "0%";
                return;
            }
            CustomerBUS bus = new CustomerBUS();
            CustomerDTO customer = bus.LoadOneCustomer(Global.IDCustomer);

            //get today
            DateTime date = DateTime.UtcNow.Date;
            string today = date.ToString("MM/dd");
            string year = date.ToString("yyyy");
            //if today is customer's birthday
            if (customer.CoopDay.Contains(today) && !customer.CoopDay.Contains(year))
            {
                discount = 4;
                btnDiscount.Content = "4%";
                return;
            }

            if (customer.Paid >= 100000000)
            {
                discount = 2;
                btnDiscount.Content = "2%";
                return;
            }

            if (customer.Paid >= 50000000)
            {
                discount = 1;
                btnDiscount.Content = "1%";
                return;
            }
            discount = 0;
            btnDiscount.Content = "0%";
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

        private void btnChooseCustomer(object sender, RoutedEventArgs e)
        {
            CustomerChoose choose = new CustomerChoose();
            choose.ShowDialog();
            btnCustomer.Content = Global.IDCustomer.ToString();
            LoadDiscount();

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
            total = total * product.OutputPrice;
            total = total / 100;
            total = total * (100 - discount);
            lblTotal.Content = total.ToString();
        }

        private void btnSell_Clicked(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text.Count() == 0 || btnProduct.Content.ToString().Count() == 0 || btnCustomer.Content.ToString().Count() == 0)
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
            result.IvenNum = result.IvenNum - amount;
            bus.UpdateProduct(result);

            CustomerBUS bUS = new CustomerBUS();
            CustomerDTO customer = bUS.LoadOneCustomer(Global.IDCustomer);
            customer.Paid = customer.Paid + Int32.Parse(lblTotal.Content.ToString());
            bUS.UpdateCustomer(customer);

            ExportBUS exportBUS = new ExportBUS();
            ExportDTO export = new ExportDTO();
            export.Amount = amount;
            export.Customer = Global.IDCustomer;
            export.DateOutput = lblDate.Content.ToString();
            export.Product = Global.IDProduct;
            export.Total = Int32.Parse(lblTotal.Content.ToString());
            exportBUS.AddNewExport(export);


            this.Close();
        }

        private void btnDiscount_Clicked(object sender, RoutedEventArgs e)
        {
            Discount discountWindow = new Discount();
            discountWindow.ShowDialog();
        }
    }
}
