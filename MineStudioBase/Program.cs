using System;
using System.Drawing;

namespace MineStudio
{
    class Program
    {

        private const String prefix = @"..\..\..\MineStudioTest\ImageCases\";
        static void Main(string[] args)
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table3.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Console.WriteLine(height);
        }

        private static void T1()
        {
            MineTable mt = new MineTable(9, 9, 10);
            Console.WriteLine(mt);
            Console.WriteLine("Miner!");
            mt.SetStatus(2, 0, CellStatus.Mine);
            mt.SetStatus(4, 0, CellStatus.Mine);
            mt.SetStatus(5, 0, CellStatus.Mine);
            mt.SetStatus(7, 0, CellStatus.Mine);
            mt.SetStatus(3, 1, CellStatus.Mine);
            mt.SetStatus(5, 2, CellStatus.Mine);
            mt.SetStatus(5, 3, CellStatus.Mine);
            mt.SetStatus(8, 4, CellStatus.Mine);
            mt.SetStatus(0, 6, CellStatus.Mine);
            mt.SetStatus(4, 8, CellStatus.Mine);
            mt.Show();
            mt.Deduce();
            mt.Show();
        }
    }
}
