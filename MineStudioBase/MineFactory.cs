using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MineStudio
{
    public class MineFactory
    {
        public static MineTable CreateFixedTable()
        {
            MineTable mineTable = new MineTable(3, 3, 2);
            mineTable.SetStatus(0, 0, CellStatus.Ground, 1);
            mineTable.SetStatus(0, 1, CellStatus.Ground, 1);
            mineTable.SetStatus(1, 0, CellStatus.Ground, 1);
            mineTable.SetStatus(1, 1, CellStatus.Mine);
            MineTable mt = new MineTable(9, 9, 10);
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
            return mt;
        }


        public static MineTable CreateRandomTable(int height,int width,int mines)
        {
            MineTable mt = new MineTable(height, width, mines);
            int total = height*width;
            int[] arr = new int[total];
            for (var i=0; i<total; i++) arr[i]=i;
            Random rd = new Random();
            for (var i = 0; i < mines; i++)
            {
                var c = rd.Next(i, total);
                mt.SetStatus(arr[c]%width, arr[c]/width, CellStatus.Mine);
                if (c!=i) arr[c] = arr[i];
            }
            mt.FullDe();
            return mt;
        }

        public static MineTable CreateFromBitmap(Bitmap bitmap)
        {
            return null;
        }
    }
}
