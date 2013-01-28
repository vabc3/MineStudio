using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace MineStudio.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MineTable _mineTable;
        private ImageSource _defaultSource;

        public MainWindow()
        {
            InitializeComponent();
            _defaultSource = Image1.Source;
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(.5);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();

            DispatcherTimer d_canv = new DispatcherTimer();
            d_canv.Interval = TimeSpan.FromSeconds(1);
            d_canv.Tick += new EventHandler(dt_Canvas);
            dt.Start();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonRandom_Click(object sender, RoutedEventArgs e)
        {
            _mineTable = new MineTable(3, 3, 2);
            Console.WriteLine(Properties.Resources.MainWindow_ButtonRandom_Click_Miner_);
            _mineTable.SetStatus(0, 0, CellStatus.Ground, 1);
            _mineTable.SetStatus(0, 1, CellStatus.Ground, 1);
            _mineTable.SetStatus(1, 0, CellStatus.Ground, 1);
            _mineTable.SetStatus(1, 1, CellStatus.Mine);
            DataGrid1.ItemsSource = _mineTable.Table;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            int a = 2;
        }

        private void ButtonDeduce_Click(object sender, RoutedEventArgs e)
        {
            if (_mineTable != null)
                _mineTable.Deduce();
            //  DataGrid1.ItemsSource = _mineTable.Table;
        }

        private void ButtonStat_Click(object sender, RoutedEventArgs e)
        {
            if (_mineTable != null)
                _mineTable.UpdateCount();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string strClass, string strWindow);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref NativeRECT rect);

        private Bitmap bitmap;
        private IntPtr fd1, fd;

        private void dt_Tick(object sender, EventArgs e)
        {
            NativeRECT rect = new NativeRECT();
            fd1 = FindWindow("minesweeper", "扫雷");
            fd = FindWindowEx(fd1, IntPtr.Zero, "static", string.Empty);
            if (fd==IntPtr.Zero)
            {
                Image1.Source=_defaultSource;
                Image1.Cursor = Cursors.Hand;
                bitmap=null;
                return;
            }
            Image1.Cursor = Cursors.Arrow;

            GetWindowRect(fd, ref rect);

            int h = rect.bottom - rect.top;
            int w = rect.right-rect.left;
            Rectangle r = new Rectangle(rect.left, rect.top, w, h);

            bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(r.Left, r.Top, 0, 0, r.Size, CopyPixelOperation.SourceCopy);
            }
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ip, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            Image1.Source = bitmapSource;
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            dt_Tick(null, null);
        }


        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        private void Image1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bitmap == null)
            {
                var qq = Environment.GetEnvironmentVariables();
                string pd = Environment.GetEnvironmentVariable("ProgramW6432");
                Process.Start(pd + @"\Microsoft Games\Minesweeper\MineSweeper.exe");
            }
            long mX = 40;
            long mY = 40;
            bool returnValue = PostMessage(new HandleRef(null,fd1), 0x0202, IntPtr.Zero,  new IntPtr((mX & 0xFFFF) + (mY & 0xFFFF) * 0x10000));
        }




        private void dt_Canvas(object sender, EventArgs e)
        {
            
        }

    }
}
