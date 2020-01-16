using TestApp;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void CompareDifferenceTestGood()
        {
            Assert.IsTrue(TestApp.Class1.CompareSquare(2, 2));
        }

        [TestMethod()]
        public void CompareDifferenceTestBad()
        {
            Assert.IsTrue(TestApp.Class1.CompareSquare(3, 10));
        }


        [TestMethod]
        public void CountSpaceTestGood()
        {
            Assert.AreEqual(TestApp.Class1.CountSpace("After all this time? Always."), 4);
        }
        [TestMethod]
        public void CountSpaceTestBad()
        {
            Assert.AreEqual(TestApp.Class1.CountSpace("After all this time? Always."), 10);
        }



        [TestMethod]
        public void CountDotTestGood()
        {
            Assert.AreEqual(TestApp.Class1.CountDot("After all this time? Always."), 1);
        }

        [TestMethod]
        public void CountDotTestBad()
        {
            Assert.AreEqual(TestApp.Class1.CountDot("After all this time? Always."), 12);
        }


        [TestMethod()]
        public void DifferenceTestGood()
        {
            Assert.AreEqual(TestApp.Class1.Difference(9, 9), 0);
        }

        [TestMethod()]
        public void DifferenceTestBad()
        {
            Assert.AreEqual(TestApp.Class1.Difference(9, 9), 18);
        }



        [TestMethod()]
        public void SameTestGood()
        {
            Assert.AreEqual(TestApp.Class1.Same("Lumos", "Lumos"), true);
        }

        [TestMethod()]
        public void SameTestBad()
        {
            Assert.AreEqual(TestApp.Class1.Same("Lumos", "Maximus"), true);
        }




        [TestMethod()]
        public void LoadfileTestGood()
        {
            Assert.AreEqual(TestApp.Class1.loadfile("D:\\Lab2\\magic.txt"), "Alohomora");
        }

        [TestMethod()]
        public void LoadfileTestBad()
        {
            Assert.AreEqual(TestApp.Class1.loadfile("D:\\Lab2\\magic.txt"), "Lumos");
        }



        [TestMethod()]
        public void CountlinesTestGood()
        {
            Assert.AreEqual(TestApp.Class1.countlines("D:\\Lab2\\magic.txt"), 1);
        }

        [TestMethod()]
        public void CountlinesTestBad()
        {
            Assert.AreEqual(TestApp.Class1.countlines("D:\\Lab2\\magic.txt"), 4);
        }



      

        [TestMethod()]
        public void NotNullTestGood()
        {
            Assert.IsNotNull(TestApp.Class1.loadfile("D:\\Lab2\\notnullhere.txt"));
        }

        [TestMethod()]
        public void NotNullTestBad()
        {
            Assert.IsNotNull(TestApp.Class1.loadfile("D:\\Lab2\\nu11.txt"));
        }



        [TestMethod()]
        public void MinNumTestGood()
        {
            Assert.AreEqual(TestApp.Class1.MinNum(new int[] { 5, 2, 9, 1, 4, 3, 10, 6, 8 }), 1);
        }

        [TestMethod()]
        public void MinNumTestBad()
        {
            Assert.AreEqual(TestApp.Class1.MinNum(new int[] { 5, 2, 9, 1, 4, 3, 10, 6, 8 }),10);
        }




        [TestMethod]
        public void PalindromTestGood()
        {
            Assert.IsTrue(TestApp.Class1.Palindrom("довод"));
        }

        [TestMethod]
        public void PalindromTestBad()
        {
            Assert.IsTrue(TestApp.Class1.Palindrom("Доводы"));
        }



        
    }
}
