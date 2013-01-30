using System;
using System.Drawing;
using System.Drawing.Imaging;
using MineStudio.Identifier;

namespace MineStudio
{
    public class MineMaker
    {
        private static IMineIdentifier _mi = MineIdentifierFactory.GetDefaultIdentifier();

        public MineMaker(Bitmap bitmap)
        {
            _bitmap = bitmap;
            _mi.GetTableInfo(bitmap, out width, out height, out il, out it, out iw, out ih);
            //Console.WriteLine("{0},{1}-({2},{3})", il, it, iw, ih);
            dw = 1.0f*iw/width;
            dh = 1.0f*ih/height;
        }

        private const int sk = 2;
        public Bitmap GetGrid(int x, int y)
        {
            return _bitmap.Clone(new RectangleF(il + x*dw +sk, it + y*dh+sk, dw-sk, dh-sk), PixelFormat.Format32bppArgb);
        }

        public MineTable GetTable()
        {

            return null;
        }

        private int width, height, iw, ih, it, il;
        private float dw, dh;
        private Bitmap _bitmap;
    }
}