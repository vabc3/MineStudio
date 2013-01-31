using System;
using System.Drawing;
using System.Drawing.Imaging;
using MineStudio.Identifier;

namespace MineStudio
{
    public class MineMaker
    {
        private static MineScanner _mi = new MineScanner();

        private static MineTable _mt=null;

        public MineMaker(Bitmap bitmap)
        {
            _bitmap = bitmap;
            if(_mi.GetTableInfo(bitmap, out width, out height, out il, out it, out iw, out ih)){
            //Console.WriteLine("{0},{1}-({2},{3})", il, it, iw, ih);
            dw = 1.0f*iw/width;
            dh = 1.0f*ih/height;
            _mt = new MineTable(height, width);

            for (var i=0; i<height; i++)
                for (var j=0; j<width; j++) {
                    CellStatus status;
                    int n;
                    _mi.GetCellInfo(GetGrid(j, i), out status, out n);
                    _mt.SetStatus(j, i, status, n);
                }
            }

        }

        private const int sk = 2;
        public Bitmap GetGrid(int x, int y)
        {
            return _bitmap.Clone(new RectangleF(il + x*dw +sk, it + y*dh+sk, dw-sk, dh-sk), PixelFormat.Format32bppArgb);
        }

        public MineTable GetTable()
        {

            return _mt;
        }

        public readonly int width, height, iw, ih, it, il;
        public readonly float dw, dh;
        private Bitmap _bitmap;
    }
}