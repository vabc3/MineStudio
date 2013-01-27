using System;
using System.Data;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;

namespace MineStudio.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MineTable _mineTable;
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
            _mineTable  = new MineTable(9, 9, 10);
            Console.WriteLine(Properties.Resources.MainWindow_ButtonRandom_Click_Miner_);
            _mineTable.SetStatus(2, 0, CellStatus.Mine);
            _mineTable.SetStatus(4, 0, CellStatus.Mine);
            _mineTable.SetStatus(5, 0, CellStatus.Mine);
            _mineTable.SetStatus(7, 0, CellStatus.Mine);
            _mineTable.SetStatus(3, 1, CellStatus.Mine);
            _mineTable.SetStatus(5, 2, CellStatus.Mine);
            _mineTable.SetStatus(5, 3, CellStatus.Mine);
            _mineTable.SetStatus(8, 4, CellStatus.Mine);
            _mineTable.SetStatus(0, 6, CellStatus.Mine);
            _mineTable.SetStatus(4, 8, CellStatus.Mine);
            DataGrid1.ItemsSource = _mineTable.Table;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            int a = 2;
        }

        private void ButtonDeduce_Click(object sender, RoutedEventArgs e)
        {
            if (_mineTable!=null)
                _mineTable.Conduce();
          //  DataGrid1.ItemsSource = _mineTable.Table;
        }



    }
}
