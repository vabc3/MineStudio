using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfTest
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Person)this.FindResource("zy2");

            MessageBox.Show(string.Format("I am {0},Age is {1}", person.Name, person.Age.ToString()));
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class MyValidation : ValidationRule
    {
        public int min { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            int c;

            if (!int.TryParse((string)value, out c))
            {
                return new ValidationResult(false, "Not num");
            }

            if (c<min)
                return new ValidationResult(false, String.Format("{0}<{1}", c, min));
            return new ValidationResult(true, null);

        }
    }
}
