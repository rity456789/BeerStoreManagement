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

namespace GUI
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
        }

        private void RangeCheck_Click(object sender, RoutedEventArgs e)
        {
            chkToday.IsChecked = false;
            chkThisWeek.IsChecked = false;
            chkThisMonth.IsChecked = false;
            CheckBox temp = sender as CheckBox;
            temp.IsChecked = true;
        }

        private void TypeCheck_Click(object sender, RoutedEventArgs e)
        {
            chkFinance.IsChecked = false;
            chkUnit.IsChecked = false;
            chkSold.IsChecked = false;
            chkToday.IsChecked = false;
            chkThisWeek.IsChecked = false;
            chkThisMonth.IsChecked = false;
            CheckBox temp = sender as CheckBox;
            temp.IsChecked = true;

            if (temp == chkSold)
            {
                chkToday.Visibility = Visibility.Hidden;
                chkThisMonth.Visibility = Visibility.Hidden;
                chkThisWeek.IsChecked = true;
            }
            else
            {
                chkToday.Visibility = Visibility.Visible;
                chkThisMonth.Visibility = Visibility.Visible;
            }
        }


        private void btnChart_Clicked(object sender, RoutedEventArgs e)
        {
            if (chkUnit.IsChecked == false && chkFinance.IsChecked == false && chkSold.IsChecked == false)
            {
                lblNotice.Content = "Please choose the type";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }

            if (chkToday.IsChecked == false && chkThisWeek.IsChecked == false && chkThisMonth.IsChecked == false)
            {
                lblNotice.Content = "Please choose the range";
                lblNotice.Visibility = Visibility.Visible;
                return;
            }
       

            if (chkUnit.IsChecked == true)
            {
                DateTime DateFrom = new DateTime();
                DateTime DateTo = new DateTime();
                if (chkThisWeek.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo.AddDays(-7);
                }
                else if (chkThisMonth.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo.AddMonths(-1);
                }
                else if (chkToday.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo;                  
                }
                PieChart unitPie = new PieChart(DateFrom, DateTo, false);
                unitPie.ShowDialog();
                return;
            }

            if (chkFinance.IsChecked == true)
            {
                DateTime DateFrom = new DateTime();
                DateTime DateTo = new DateTime();
                if (chkThisWeek.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo.AddDays(-7);
                }
                else if (chkThisMonth.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo.AddMonths(-1);
                }
                else if (chkToday.IsChecked == true)
                {
                    DateTo = DateTime.Now.Date;
                    DateFrom = DateTo;
                }
                PieChart financePie = new PieChart(DateFrom, DateTo, true);
                financePie.ShowDialog();
                return;
            }

            if (chkSold.IsChecked == true)
            {
                ColumnChart soldColumn = new ColumnChart();
                soldColumn.ShowDialog();
                return;
            }
        }
    }
}
