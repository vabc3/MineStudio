using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineStudio;
using MineStudio.Identifier;

namespace Test
{


    /// <summary>
    ///这是 LineMineIdentifierTest 的测试类，旨在
    ///包含所有 LineMineIdentifierTest 单元测试
    ///</summary>
    [TestClass()]
    public class CellTest
    {
        private const String Prefix = @"..\..\..\MineStudioTest\ImageCases\";
        private static readonly IMineIdentifier Target= MineIdentifierFactory.GetDefaultIdentifier();

        private static Stopwatch sw = new Stopwatch();

        [ClassInitialize]
        public static void Ci(TestContext context)
        {
            sw.Start();
        }

        [ClassCleanup]
        public static void Cd()
        {
            sw.Stop();
            System.Windows.Forms.MessageBox.Show("总运行时间：" + sw.Elapsed);
        }

        private static void NumTest(int x,int app=1)
        {
            string url = string.Format("{0}{1}-{2}.png",Prefix,x,app);
            Bitmap data = new Bitmap(url);
            CellStatus status;
            int n;
            bool actual = Target.GetCellInfo(data, out status, out n);
            Assert.AreEqual(true, actual);
 
            Assert.AreEqual(x, n);
        }


        [TestMethod()]
        public void CellTest0()
        {
            NumTest(0);
            NumTest(0,2);
        }

        [TestMethod()]
        public void CellTest1()
        {
            NumTest(1);
            NumTest(1,2);
        }

        [TestMethod()]
        public void CellTest2()
        {
            NumTest(2);
            NumTest(2,2);
        }

        [TestMethod()]
        public void CellTest3()
        {
            NumTest(3);
            NumTest(3,2);
            NumTest(3,3);
        }

        [TestMethod()]
        public void CellTest4()
        {
            NumTest(4);
            NumTest(4, 2);
        }

        [TestMethod()]
        public void CellTest5()
        {
            NumTest(5);
            NumTest(5,2);
        }

        [TestMethod()]
        public void CellTest6()
        {
            NumTest(6);
            NumTest(6,2);
        }

        [TestMethod()]
        public void CellTest7()
        {
            NumTest(7);
            NumTest(7,2);
        }

        [TestMethod()]
        public void CellTest8()
        {
            NumTest(8);
            NumTest(8,2);
        }
       
    }
}
