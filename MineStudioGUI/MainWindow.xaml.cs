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

namespace MineStudioGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SolidColorBrush sb=new SolidColorBrush();
            label1.Background=sb;
            ColorAnimation cl = new ColorAnimation();
            cl.To=Colors.White;
            cl.Duration=TimeSpan.FromSeconds(2);
            RegisterName("MyAnimatedBrush", sb);
            Storyboard.SetTargetName(cl, "MyAnimatedBrush");
            //Storyboard.SetTarget(cl, sb);
            Storyboard.SetTargetProperty(cl, new PropertyPath(SolidColorBrush.ColorProperty));
            button2.Click+=(object sender, RoutedEventArgs e) =>
            {
                sb.Color=Colors.Sienna;
                label1.Background=sb;
                Storyboard s= new Storyboard();
                s.Children.Add(cl);
                s.Begin(button2);
            };

               // new RoutedEventHandler(button2_Click);

            //Storyboard s= new Storyboard();
            //s.Children.Add(cl);
            //s.Begin();
        }

        void button2_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Button mybutton = (Button)sender;
            DoubleAnimation myani = new DoubleAnimation(); //实例化一个DoubleAninmation对象
            myani.From = mybutton.Width;//开始值
            myani.To = mybutton.Width+400;//结束值
            myani.Duration = TimeSpan.FromSeconds(1); //所用时间
            Storyboard.SetTarget(myani, mybutton);  //设置应用的对象
            //Storyboard.SetTargetProperty(myani, new PropertyPath("Width1"));  //设置应用的依赖项属性
            Storyboard.SetTargetProperty(myani, new PropertyPath(Button.WidthProperty));
            Storyboard s = new Storyboard();// 实例化一个故事板
            s.Children.Add(myani);//将先前动画添加进来
            s.Begin(); //启动故事版
        }

    }
}
