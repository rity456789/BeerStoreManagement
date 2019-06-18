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
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Window
    {
        public PieChart(DateTime DayFrom, DateTime DayTo, bool isFinanceChart)
        {
            InitializeComponent();
            if(isFinanceChart)
            {
                LoadFinancePieChartData(DayFrom, DayTo);
            }
            else
            {
                LoadUnitPieChartData(DayFrom, DayTo);
            }
            
        }

        private void LoadUnitPieChartData(DateTime DayFrom, DateTime DayTo)
        {
            //Luu id san pham va so luong san pham do ban trong hom nay
            List<KeyValue> listKey = new List<KeyValue>();


            ExportBUS bus = new ExportBUS();
            List<ExportDTO> list = bus.LoadAllExport(); //luu danh sach hoa don



            for (int i = 0; i < list.Count(); i++)
            {
                //Kiem tra co nam trong pham vi bao cao hay khong
                DateTime soldDay = DateTime.Parse(list[i].DateOutput);
                if (soldDay < DayFrom || soldDay > DayTo)
                {
                    continue;
                }


                bool check = false;
                for (int j = 0; j < listKey.Count(); j++)
                {
                    //Xem ton tai trong list hay chua
                    if (listKey[j].key == list[i].Product)
                    {//Da ton tai
                        check = true;
                        listKey[j].value += list[i].Amount;
                        break;
                    }
                }

                //neu chua ton tai thi them vao
                if (check == false)
                {
                    listKey.Add(new KeyValue() { key = list[i].Product, value = list[i].Amount });
                }
            }

            ProductBUS productBUS = new ProductBUS();

            var listStatistic = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < listKey.Count(); i++)
            {
                ProductDTO product = productBUS.LoadOneProduct(listKey[i].key);
                listStatistic.Add(new KeyValuePair<string, int>(product.Name, listKey[i].value));
            }



            ((PieSeries)mcChart.Series[0]).ItemsSource = listStatistic;
        }

        private void LoadFinancePieChartData(DateTime DayFrom, DateTime DayTo)
        {
            //Luu id san pham va so luong san pham do ban trong hom nay
            List<KeyValue> listKey = new List<KeyValue>();


            ExportBUS bus = new ExportBUS();
            List<ExportDTO> list = bus.LoadAllExport(); //luu danh sach hoa don

            ProductBUS productBUS = new ProductBUS();


            for (int i = 0; i < list.Count(); i++)
            {
                //Kiem tra co nam trong pham vi bao cao hay khong
                DateTime soldDay = DateTime.Parse(list[i].DateOutput);
                if (soldDay < DayFrom || soldDay > DayTo)
                {
                    continue;
                }


                bool check = false;
                for (int j = 0; j < listKey.Count(); j++)
                {
                    //Xem ton tai trong list hay chua
                    if (listKey[j].key == list[i].Product)
                    {//Da ton tai
                        check = true;
                        int income; //income of that bill
                        ProductDTO product = productBUS.LoadOneProduct(list[i].Product);
                        income = list[i].Total - product.InputPrice * list[i].Amount;
                        listKey[j].value += income;
                        break;
                    }
                }

                //neu chua ton tai thi them vao
                if (check == false)
                {
                    int income; //income of that bill
                    ProductDTO product = productBUS.LoadOneProduct(list[i].Product);
                    income = list[i].Total - product.InputPrice * list[i].Amount;
                    listKey.Add(new KeyValue() { key = list[i].Product, value = income });
                }
            }


            var listStatistic = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < listKey.Count(); i++)
            {
                ProductDTO product = productBUS.LoadOneProduct(listKey[i].key);
                listStatistic.Add(new KeyValuePair<string, int>(product.Name, listKey[i].value));
            }



            ((PieSeries)mcChart.Series[0]).ItemsSource = listStatistic;
        }
    }
}