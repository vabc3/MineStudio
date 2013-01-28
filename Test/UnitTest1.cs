using MineStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Test
{
    /// <summary>
    ///这是 W7MineIdentifierTest 的测试类，旨在
    ///包含所有 W7MineIdentifierTest 单元测试
    ///</summary>
    [TestClass()]
    public class W7MineIdentifierTest
    {

        /// <summary>
        ///GetCellInfo 的测试
        ///</summary>
        //[TestMethod()]
        //public void GetCellInfoTest()
        //{
        //    W7MineIdentifier target = new W7MineIdentifier(); 
        //    Bitmap data = null; 
        //    CellStatus status; 
        //    CellStatus statusExpected = new CellStatus(); 
        //    int n = 0; // TODO: 初始化为适当的值
        //    bool expected = false; // TODO: 初始化为适当的值
        //    bool actual;
        //    actual = target.GetCellInfo(data, out status, n);
        //    Assert.AreEqual(statusExpected, status);
        //    Assert.AreEqual(expected, actual);
        //}
        private const String prefix = @"..\..\..\MineStudioTest\ImageCases\";

        /// <summary>
        ///GetTableInfo 的测试
        ///</summary>
        [TestMethod()]
        public void GetTableInfoTest1()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table1.bmp");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(9, width);
            Assert.AreEqual(9, height);
        }

        [TestMethod()]
        public void GetTableInfoTest2()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table2.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(16, width);
            Assert.AreEqual(16, height);
        }

        [TestMethod()]
        public void GetTableInfoTest3()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table3.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(30, width);
            Assert.AreEqual(16, height);
        }

        [TestMethod()]
        public void GetTableInfoTest4()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table4.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(30, width);
            Assert.AreEqual(24, height);
        }

        [TestMethod()]
        public void GetTableInfoTest5()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table5.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(18, width);
            Assert.AreEqual(17, height);
        }

        [TestMethod()]
        public void GetTableInfoTest6()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table6.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void GetTableInfoTest7()
        {
            W7MineIdentifier target = new W7MineIdentifier();
            Bitmap data = new Bitmap(prefix+"Table7.png");
            int width,height;
            bool actual = target.GetTableInfo(data, out width, out height);
            Assert.AreEqual(true, actual);
            Assert.AreEqual(15, width);
            Assert.AreEqual(14, height);
        }
    }
}
