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
            System.Console.WriteLine(mt);

            System.Console.WriteLine("Miner!");

          
        }
    }
}
