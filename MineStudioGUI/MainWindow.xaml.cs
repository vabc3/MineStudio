using System;
using System.Data;
using System.Windows;
using System.ComponentModel;

namespace MineStudio.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void ButtonRandom_Click(object sender, RoutedEventArgs e)
        {
            MineTable mt=new MineTable(9, 9, 10);
            Console.WriteLine(Properties.Resources.MainWindow_ButtonRandom_Click_Miner_);
            mt.SetStatus(2, 0, MineStatus.ISMINE);
            mt.SetStatus(4, 0, MineStatus.ISMINE);
            mt.SetStatus(5, 0, MineStatus.ISMINE);
            mt.SetStatus(7, 0, MineStatus.ISMINE);
            mt.SetStatus(3, 1, MineStatus.ISMINE);
            mt.SetStatus(5, 2, MineStatus.ISMINE);
            mt.SetStatus(5, 3, MineStatus.ISMINE);
            mt.SetStatus(8, 4, MineStatus.ISMINE);
            mt.SetStatus(0, 6, MineStatus.ISMINE);
            mt.SetStatus(4, 8, MineStatus.ISMINE);
            mt.Show();
            mt.Conduce();

            int[,] TABLE = mt.Table;
            DataTable dt = new DataTable();
            for (int i = 0; i < TABLE.GetLength(1); i++)
                dt.Columns.Add(i.ToString(), typeof(int));
            for (int i = 0; i < TABLE.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < TABLE.GetLength(1); j++)
                    dr[j] = TABLE[i, j];
                dt.Rows.Add(dr);
            }

            
            DataGrid1.ColumnWidth = 40;
            DataGrid1.RowHeight   = 40;
            //DataGrid1.ItemsSource = dt.AsDataView();

            customerList = new BindingList<DemoCustomer>();
            customerList.Add(new DemoCustomer());
            customerList.Add(new DemoCustomer());
            customerList.Add(new DemoCustomer());
            DataGrid1.ItemsSource = customerList;

        }

        private BindingList<DemoCustomer> customerList;

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            int a = 2;
        }
    }

    public class DemoCustomer
    {
        public Guid Guid { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public DemoCustomer()
        {
            Guid = Guid.NewGuid();
            CustomerName = "中";
            PhoneNumber = "(312)555-0100";
        }
    }
}

