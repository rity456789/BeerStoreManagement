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
using System.Windows.Controls.DataVisualization.Charting;


namespace GUI
{
    /// <summary>
    /// Interaction logic for ColumnChart.xaml
    /// </summary>
    public partial class ColumnChart : Window
    {
        public ColumnChart()
        {
            InitializeComponent();
            LoadColumnChartData();
        }

        private void LoadColumnChartData()
        {
            //Luu so tien loi trong tung ngay
            List<KeyValue> listKey = new List<KeyValue>();
            for (int i = 0; i < 7; i++)
            {
                listKey.Add(new KeyValue() { key = i, value = 0 });
            }

            ProductBUS productBUS = new ProductBUS();
            ExportBUS bus = new ExportBUS();
            List<ExportDTO> list = bus.LoadAllExport(); //luu danh sach hoa don

            DateTime DayTo = DateTime.Now.Date;
            DateTime DayFrom = DayTo.AddDays(-7);



            for (int i = 0; i < list.Count(); i++)
            {
                //Kiem tra co nam trong pham vi bao cao hay khong
                DateTime soldDay = DateTime.Parse(list[i].DateOutput);
                if (soldDay < DayFrom || soldDay > DayTo)
                {
                    continue;
                }

                //Kiem tra la ngay nao de add vao
                for (int j = 0; j < 7; j++)
                {
                    DateTime thisDay = DateTime.Now.Date.AddDays(-6 + j);
                    if (thisDay == soldDay) 
                    {
                        int income; //tien loi cua hoa don do
                        ProductDTO product = productBUS.LoadOneProduct(list[i].Product);
                        income = list[i].Total - product.InputPrice * list[i].Amount;
                        listKey[j].value += income;
                        break;
                    }
                }
            }

            var listStatistic = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < 7; i++)
            {
                DateTime thisDay = DateTime.Now.Date.AddDays(-6 + i);
                string showedDay = thisDay.ToString("MM/dd/yyyy");
                listStatistic.Add(new KeyValuePair<string, int>(showedDay, listKey[i].value));
            }



            ((ColumnSeries)mcChart.Series[0]).ItemsSource = listStatistic;
        }
    }
}
