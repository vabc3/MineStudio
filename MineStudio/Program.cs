using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            MineTable mt=new MineTable(9,9,10);
            Console.WriteLine(mt);
            Console.WriteLine("Miner!");
            mt.SetStatus(2, 0, MineStatus.ISMINE);
            mt.SetStatus(4, 0, MineStatus.ISMINE);
            mt.SetStatus(5, 0, MineStatus.ISMINE);
            mt.SetStatus(7, 0, MineStatus.ISMINE);
            mt.SetStatus(3, 1, MineStatus.ISMINE);
            mt.SetStatus(5, 2, MineStatus.ISMINE);
            mt.SetStatus(5, 3, MineStatus.ISMINE);
            mt.SetStatus(8, 4, MineStatus.ISMINE);
            mt.SetStatus(0, 6, MineStatus.ISMINE);
            mt.SetStatus(4, 8, MineStatus.ISMINE);
            mt.Show();
            mt.Conduce();
            mt.Show();
        }
    }
}
