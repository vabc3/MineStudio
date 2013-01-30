using System;
using System.Collections.Generic;
using System.Drawing;
using MineStudio.Identifier;
using System.Linq;

namespace MineStudio
{
    class Program
    {
        private const String Prefix = @"..\..\..\MineStudioTest\ImageCases\";

        static void Main(string[] args)
        {
            IMineIdentifier target = MineIdentifierFactory.GetDefaultIdentifier();
            Bitmap data = new Bitmap(Prefix + "5-1.png");
            CellStatus s;
            int n;
            target.GetCellInfo(data, out s, out n);
        }

        private static void D1()
        {
            IMineIdentifier target = MineIdentifierFactory.GetDefaultIdentifier();
            Bitmap data = new Bitmap(Prefix + "Table0.png");
            MineMaker mm = new MineMaker(data);
            var t = mm.GetGrid(17, 7);
            CellStatus ct;
            int c;

            t.Save("qq.png");
            target.GetCellInfo(t, out ct, out c);
        }
    }
}
