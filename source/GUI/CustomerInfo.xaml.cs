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
    /// Interaction logic for CustomerInfo.xaml
    /// </summary>
    public partial class CustomerInfo : Window
    {
        int ID;
        public CustomerInfo(int id)
        {
            InitializeComponent();
            LoadInfo(id);
            ID = id;
        }

        private void LoadInfo(int id)
        {
            CustomerBUS bus = new CustomerBUS();
            CustomerDTO result = bus.LoadOneCustomer(id);
            txtID.Content = result.ID.ToString();
            txtName.Text = result.Name;
            txtEmail.Text = result.Email;
            txtPhone.Text = result.Phone;
            lblCoopday.Content = result.CoopDay;
            txtAddress.Text = result.Address;
            lblPaid.Content = result.Paid.ToString();
        }

        private void btnSave_Clicked(object sender, RoutedEventArgs e)
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
            result.ID = ID;
            result.Name = txtName.Text;
            result.Email = txtEmail.Text;
            result.Phone = txtPhone.Text;
            result.CoopDay = lblCoopday.Content.ToString();
            result.Address = txtAddress.Text;
            result.Paid = Int32.Parse(lblPaid.Content.ToString());
            bus.UpdateCustomer(result);
            this.Close();
        }

        private void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            CustomerBUS bus = new CustomerBUS();
            bus.DeleteCustomer(ID);
            this.Close();
        }
    }
}
