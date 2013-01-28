using MineStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace MineStudio.Test
{
    /// <summary>
    ///这是 W7MineIdentifierTest 的测试类，旨在
    ///包含所有 W7MineIdentifierTest 单元测试
    ///</summary>
    [TestClass()]
    public class W7MineIdentifierTest
    {
        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

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
    }
}
