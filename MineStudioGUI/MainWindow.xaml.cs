using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Brushes = System.Windows.Media.Brushes;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;
using Rectangle = System.Drawing.Rectangle;
using System.Globalization;



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
            //TODO: Console.WriteLine(Properties.Resources.MainWindow_ButtonRandom_Click_Miner_);
            _mineTable = MineFactory.CreateRandomTable(16,16,40);
            DataGrid1.ItemsSource = _mineTable.Table;
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
            bool returnValue = PostMessage(new HandleRef(null, fd1), 0x0202, IntPtr.Zero, new IntPtr((mX & 0xFFFF) + (mY & 0xFFFF) * 0x10000));
        }


        private void dt_Canvas(object sender, EventArgs e)
        {

        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Canvas_Update();
        }

        private void DrawBoard()
        {
            if (_mineTable==null) return;

            double bl = 1.0*_mineTable.Width/_mineTable.Height;
            double bc = _canvasWidth/_canvasHeight;
            if (bl > bc)
            {
                //Depends on width
                _boardWidth     = _canvasWidth*Factor;
                _boardHeight    = _boardWidth / bl;
            }
            else
            {
                _boardHeight = _canvasHeight*Factor;
                _boardWidth = _boardHeight * bl;
            }

            _boardTop   = (_canvasHeight-_boardHeight) / 2;
            _boardLeft = (_canvasWidth - _boardHeight)/2;


            var myRectangleGeometry = new RectangleGeometry { Rect = new Rect(_boardLeft, _boardTop, _boardWidth, _boardHeight) };

            var myPath = new Path
                {
                    Stroke = Brushes.Black,
                    StrokeDashArray = new DoubleCollection(2) { 2 },
                    StrokeThickness = 1,
                    Data = myRectangleGeometry
                };
            Canvas1.Children.Add(myPath);

            for (int i = 1; i < _mineTable.Height; i++)
            {
                var myLineGeometry = new LineGeometry
                    {
                        StartPoint = new Point(_boardLeft, _boardTop+i*dh),
                        EndPoint = new Point(_boardLeft + _boardWidth, _boardTop+ i*dh)
                    };
                var pa = new Path
                {
                    Stroke = Brushes.Black,
                    StrokeDashArray = new DoubleCollection(2) { 2 },
                    Data = myLineGeometry
                };
                Canvas1.Children.Add(pa);
            }


            for (var i = 1; i < _mineTable.Width; i++)
            {
                var myLineGeometry = new LineGeometry
                {
                    StartPoint = new Point(_boardLeft+ i*dw, _boardTop),
                    EndPoint = new Point(_boardLeft + i*dw, _boardTop+_boardHeight)
                };
                var pa = new Path
                {
                    Stroke = Brushes.Black,
                    StrokeDashArray = new DoubleCollection(2) { 2 },
                    Data = myLineGeometry
                };
                Canvas1.Children.Add(pa);
            }

            //var myLineGeometry = new LineGeometry{StartPoint=new Point}

        }


        private void DrawCell(MineCell cell)
        {
            double px = (cell.X + .5) * dw+_boardLeft;
            double py = (cell.Y + .5)* dh+_boardTop;

            string str = "U";
            switch (cell.Status)
            {
                case CellStatus.Unknow:
                    break;
                case CellStatus.Mine:
                    str = "M";
                    break;
                case CellStatus.Ground:
                    str = cell.N.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var formattedText = new FormattedText(
            str,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface("Verdana"),
            16,
            Brushes.Black);
            var tb = formattedText.BuildGeometry(new Point(px, py));
            var pa = new Path
                {
                    Stroke = Brushes.Black,
                    Data = tb
                };
            Canvas1.Children.Add(pa);

            if (Selected == cell)
            {
                double pw=.4*dw;
                double ph=.4*dh;
                RectangleGeometry rg =
                    new RectangleGeometry(new Rect(new Point(px - pw, py - ph), new Point(px + pw, py + ph)));
                var pc = new Path
                {
                    Stroke = Brushes.Blue,
                    Data = rg
                };
                Canvas1.Children.Add(pc);
            }

            if (cell.PredStatus==CellStatus.Unknow) return;

            EllipseGeometry eg = new EllipseGeometry(
                new Point(px, py), 10, 10
                );
            var pb = new Path
                {
                    Fill = (cell.PredStatus == CellStatus.Mine ? Brushes.Red : Brushes.Green),
                    Data = eg,

                };
            Canvas1.Children.Add(pb);

        }

        private double dh;
        private double dw;

        private MineCell Selected=null;

        private void Canvas_Update()
        {
            Canvas1.Children.Clear();

            if (_mineTable != null)
            {
                dh = _boardHeight/_mineTable.Height;
                dw = _boardWidth/_mineTable.Width;
                DrawBoard();
                foreach (var item in _mineTable.Table)
                DrawCell(item);
            }
            
        }


        private double _canvasHeight, _canvasWidth;
        private double _boardTop, _boardLeft;
        private double _boardHeight, _boardWidth;
        private const double Factor = 0.9;

        private void Canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("Canvas_sizeChange");
            _canvasHeight = Canvas1.ActualHeight;
            _canvasWidth = Canvas1.ActualWidth;
            Canvas_Update();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            bitmap.Save("tbc.bmp");
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dt = sender as DataGrid;
            if (dt.SelectedIndex > 0)
            {
                Selected = _mineTable.Table[dt.SelectedIndex];
            }
            Canvas_Update();
        }

        private void Canvas1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(Canvas1);
            int x = (int)((p.X - _boardLeft)/dw);
            int y = (int)((p.Y - _boardTop)/dh);
            if (_mineTable.IndexValid(x, y))
                DataGrid1.SelectedIndex = _mineTable.GetIndex(x, y);
            else
            {
                DataGrid1.SelectedIndex = -1;
                Selected=null;
            }

        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("Grid_sizeChange");
        }

    }
}
