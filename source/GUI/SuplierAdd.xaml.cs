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
    /// Interaction logic for SuplierAdd.xaml
    /// </summary>
    public partial class SuplierAdd : Window
    {
        public SuplierAdd()
        {
            InitializeComponent();
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


            SuplierBUS bus = new SuplierBUS();
            SuplierDTO result = new SuplierDTO();
            result.Name = txtName.Text;
            result.Email = txtEmail.Text;
            result.Phone = txtPhone.Text;
            result.Address = txtAddress.Text;
            bus.AddNewSuplier(result);
            this.Close();
        }
    }
}
