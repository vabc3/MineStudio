using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineStudio.Identifier;

namespace Test
{
    
    
    /// <summary>
    ///这是 LineMineIdentifierTest 的测试类，旨在
    ///包含所有 LineMineIdentifierTest 单元测试
    ///</summary>
    [TestClass()]
    public class LineMineIdentifierTest
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

        [TestMethod()]
        public void GetTableInfoTest0()
        {
            Bitmap data = new Bitmap(Prefix+"Table0.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(23, width);
            Assert.AreEqual(20, height);
        }

        [TestMethod()]
        public void GetTableInfoTest1()
        {
            Bitmap data = new Bitmap(Prefix+"Table1.bmp");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height,out ileft,out itop,out iwidth,out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(9, width);
            Assert.AreEqual(9, height);
        }

        [TestMethod()]
        public void GetTableInfoTest2()
        {
            Bitmap data = new Bitmap(Prefix+"Table2.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(16, width);
            Assert.AreEqual(16, height);
        }

        [TestMethod()]
        public void GetTableInfoTest3()
        {
            Bitmap data = new Bitmap(Prefix+"Table3.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(30, width);
            Assert.AreEqual(16, height);
        }

        [TestMethod()]
        public void GetTableInfoTest4()
        {
            Bitmap data = new Bitmap(Prefix+"Table4.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(30, width);
            Assert.AreEqual(24, height);
        }

        [TestMethod()]
        public void GetTableInfoTest5()
        {
            Bitmap data = new Bitmap(Prefix+"Table5.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(18, width);
            Assert.AreEqual(17, height);
        }

        [TestMethod()]
        public void GetTableInfoTest6()
        {
            Bitmap data = new Bitmap(Prefix+"Table6.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void GetTableInfoTest7()
        {
            Bitmap data = new Bitmap(Prefix+"Table7.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(15, width);
            Assert.AreEqual(14, height);
        }

        [TestMethod()]
        public void GetTableInfoTest8()
        {
            Bitmap data = new Bitmap(Prefix+"Table8.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void GetTableInfoTest9()
        {
            Bitmap data = new Bitmap(Prefix+"Table9.png");
            int width,height,ileft,itop,iwidth,iheight;
            bool actual = Target.GetTableInfo(data, out width, out height, out ileft, out itop, out iwidth, out iheight);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(27, width);
            Assert.AreEqual(21, height);
        }
    }
}
