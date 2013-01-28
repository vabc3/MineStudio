using System;
using System.Drawing;
using System.Windows;

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

        private bool Near(Color a, Color b)
        {
            int gap = 11;
            return (Math.Abs(a.R - b.R) < gap && Math.Abs(a.G - b.G) < gap && Math.Abs(a.B - b.B) < gap);

        }

        private bool isSep(Color c)
        {
            return (c.R + c.G + c.B < 11);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //BitmapImage bi = new BitmapImage(new Uri(new FileInfo("./tbc.bmp").FullName));
            //Image1.Source=bi;

            Bitmap a = new Bitmap("tbc.bmp");
            //Bitmap b = a.Clone(new Rectangle(320, 80, 300, 100), PixelFormat.Format32bppArgb);
            //b.Save("2.bmp");
            int width = a.Width;
            int height = a.Height;
            int top = 0;
            Color c;
            int old_top=0;
            Color old_c=Color.Wheat;

            do
            {
                top++;
                c = a.GetPixel(width/2-31, top);
                a.SetPixel(width/2 - 31, top, Color.Yellow);
                if (Near(old_c,c))
                {
                    old_c=c;
                }
                else{
                    //Console.WriteLine("{0}:{1}",top-old_top,old_c);
                    if(isSep(old_c)) {
                        Console.WriteLine(top);
                        a.SetPixel(width/2 - 30, top, Color.Red);
                    }
                    old_top=top;
                    old_c=c;
                }
                
            } while (c != Color.Black && top < height-1);
            Console.WriteLine(top);
            a.Save("Due.bmp");
        }

       
    }

    
}
