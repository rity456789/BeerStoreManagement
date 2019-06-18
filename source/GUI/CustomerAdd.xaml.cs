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
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window
    {
        public CustomerAdd()
        {
            InitializeComponent();
            //get today
            DateTime date = DateTime.UtcNow.Date;
            string today = date.ToString("MM/dd/yyyy");
            lblCoopday.Content = today;
            lblPaid.Content = 0;
        }

        private void btnAdd_clicked(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Count() == 0 || txtEmail.Text.Count() == 0 || txtPhone.Text.Count() == 0 || txtAddress.Text.Count() == 0)
            {
                lblNotice.Content = "Please fill all information.";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }


            int temp;
            bool isNumeric = Int32.TryParse(txtPhone.Text, out temp);
            if (!isNumeric)
            {
                lblNotice.Content = "Phone isn't number!";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }





            CustomerBUS bus = new CustomerBUS();
            CustomerDTO result = new CustomerDTO();
            result.Name = txtName.Text;
            result.Email = txtEmail.Text;
            result.Phone = txtPhone.Text;
            result.CoopDay = lblCoopday.Content.ToString();
            result.Address = txtAddress.Text;
            result.Paid = 0;
            bus.AddNewCustomer(result);
            this.Close();
        }
    }
}
