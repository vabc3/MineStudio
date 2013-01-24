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
            MineTable mt=new MineTable(4);
            System.Console.WriteLine(mt);

            System.Console.WriteLine("Miner!");

          
        }
    }
}
