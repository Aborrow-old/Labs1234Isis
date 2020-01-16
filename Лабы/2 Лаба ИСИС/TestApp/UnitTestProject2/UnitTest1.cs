using TestApp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void SummaTest()
        {
            Assert.AreEqual(TestApp.Class1.summa(5, 5), 10);
        }

        [TestMethod()]
        public void LoadfileTest()
        {
            Assert.AreEqual(TestApp.Class1.loadfile("E:\\ИСИС\\first.txt"), "Hi");
        }

        [TestMethod()]
        public void CountlinesTest()
        {
            Assert.AreEqual(TestApp.Class1.countlines("E:\\ИСИС\\second.txt"), 4);
        }

        [TestMethod()]
        public void SravnenieTest()
        {
            Assert.AreEqual(TestApp.Class1.sravnenie("E:\\ИСИС\\first.txt", "E:\\ИСИС\\second.txt"), 1);
        }

        [TestMethod()]
        public void Notnul()
        {
            Assert.IsNotNull(TestApp.Class1.countlines("E:\\ИСИС\\first.txt"));
        }

        [TestMethod()]
        public void ReferenceEquals()
        {
            Assert.ReferenceEquals(TestApp.Class1.countlines("E:\\ИСИС\\first.txt"), TestApp.Class1.countlines("E:\\ИСИС\\second.txt"));
        }

        [TestMethod()]
        public void sumtrueTest()
        {
            Assert.IsTrue(TestApp.Class1.sumtrue(2, 5));
        }

        [TestMethod()]
        public void MaxTest()
        {
            Assert.AreEqual(TestApp.Class1.max(new int[] { 1, 3, 5, 7, 9 }), 9);
        }

        [TestMethod]
        public void PalindromTest()
        {
            Assert.IsTrue(TestApp.Class1.Palindrom("дед"));
        }

        [TestMethod]
        public void CountSpaceTest()
        {
            Assert.AreEqual(TestApp.Class1.CountSpace("Привет, как дела!"), 2);
        }
    }
}
