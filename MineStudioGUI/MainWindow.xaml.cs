using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace MineStudio.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> lp=new List<Person>{
                                new Person("fd",4),
                                new Person("fr",5)
                            };
        public MainWindow()
        {
            InitializeComponent();

            
            dataGrid1.ItemsSource=lp;
        }

     
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }
    }
}


class Person
{

    public Person(string p, int p_2)
    {
        // TODO: Complete member initialization
        this.Name = p;
        this.Age = p_2;
    }
    public string Name { get; set; }
    public int  Age { get; set; }
}