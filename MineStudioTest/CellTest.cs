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
        private static readonly MineScanner Target= new MineScanner();

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
            Assert.AreEqual(CellStatus.Ground, status);
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
        public void CellTest1_3()
        {
            NumTest(1, 3);
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
        public void CellTest8_1()
        {
            NumTest(8);
        }

        [TestMethod()]
        public void CellTest8_2()
        {
            NumTest(8,2);
        }


        private void UnTest(int app=1)
        {
            string url = string.Format("{0}u-{1}.png", Prefix, app);
            Bitmap data = new Bitmap(url);
            CellStatus status;
            int n;
            bool actual = Target.GetCellInfo(data, out status, out n);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(CellStatus.Covered,status);
        }

        [TestMethod()]
        public void CellTestU()
        {
            UnTest();
        }

        [TestMethod()]
        public void CellTestU_2()
        {
            UnTest( 2);
        }

        [TestMethod()]
        public void CellTestU_3()
        {
            UnTest( 3);
        }

        [TestMethod()]
        public void CellTestU_4()
        {
            UnTest(4);
        }

        private void MiTest(int app=1)
        {
            string url = string.Format("{0}m-{1}.png", Prefix, app);
            Bitmap data = new Bitmap(url);
            CellStatus status;
            int n;
            bool actual = Target.GetCellInfo(data, out status, out n);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(CellStatus.Mine, status);
        }

        [TestMethod()]
        public void CellTestM()
        {
            MiTest();
        }

        [TestMethod()]
        public void CellTestM_2()
        {
            MiTest(2);
        }

        [TestMethod()]
        public void CellTestM_3()
        {
            MiTest(3);
        }

        [TestMethod()]
        public void CellTestM_4()
        {
            MiTest(3);
        }
    }
}
