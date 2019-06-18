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
    /// Interaction logic for SuplierInfo.xaml
    /// </summary>
    public partial class SuplierInfo : Window
    {
        int ID;
        public SuplierInfo(int id)
        {
            InitializeComponent();
            LoadInfo(id);
            ID = id;
        }

        private void LoadInfo(int id)
        {
            SuplierBUS bus = new SuplierBUS();
            SuplierDTO result = bus.LoadOneSuplier(id);
            txtID.Content = result.ID.ToString();
            txtName.Text = result.Name;
            txtEmail.Text = result.Email;
            txtPhone.Text = result.Phone;
            txtAddress.Text = result.Address;
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



            SuplierBUS bus = new SuplierBUS();
            SuplierDTO result = new SuplierDTO();
            result.ID = ID;
            result.Name = txtName.Text;
            result.Email = txtEmail.Text;
            result.Phone = txtPhone.Text;
            result.Address = txtAddress.Text;
            bus.UpdateSuplier(result);
            this.Close();
        }

        private void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            SuplierBUS bus = new SuplierBUS();
            bus.DeleteSuplier(ID);
            this.Close();
        }


    }
}
