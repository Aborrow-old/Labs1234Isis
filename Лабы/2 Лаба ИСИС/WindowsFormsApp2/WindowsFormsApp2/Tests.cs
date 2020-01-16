using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void CompareDifferenceTestGood()
        {
            Assert.IsTrue(WindowsFormsApp2.Class1.CompareSquare(2, 2));
        }


        [Test]
        public void CompareDifferenceTestBad()
        {
            Assert.IsTrue(WindowsFormsApp2.Class1.CompareSquare(3, 10));
        }


        [Test]
        public void CountSpaceTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.CountSpace("After all this time? Always."), 4);
        }
        [Test]
        public void CountSpaceTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.CountSpace("After all this time? Always."), 10);
        }



        [Test]
        public void CountDotTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.CountDot("After all this time? Always."), 1);
        }

        [Test]
        public void CountDotTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.CountDot("After all this time? Always."), 12);
        }


        [Test]
        public void DifferenceTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.Difference(9, 9), 0);
        }

        [Test]
        public void DifferenceTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.Difference(9, 9), 18);
        }



        [Test]
        public void SameTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.Same("Lumos", "Lumos"), true);
        }

        [Test]
        public void SameTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.Same("Lumos", "Maximus"), true);
        }




        [Test]
        public void LoadfileTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.loadfile("D:\\Lab2\\magic.txt"), "Alohomora");
        }

        [Test]
        public void LoadfileTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.loadfile("D:\\Lab2\\magic.txt"), "Lumos");
        }



        [Test]
        public void CountlinesTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.countlines("D:\\Lab2\\magic.txt"), 1);
        }

        [Test]
        public void CountlinesTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.countlines("D:\\Lab2\\magic.txt"), 4);
        }





        [Test]
        public void NotNullTestGood()
        {
            Assert.IsNotNull(WindowsFormsApp2.Class1.loadfile("D:\\Lab2\\notnullhere.txt"));
        }

        [Test]
        public void NotNullTestBad()
        {
            Assert.IsNotNull(WindowsFormsApp2.Class1.loadfile("D:\\Lab2\\nu11.txt"));
        }



        [Test]
        public void MinNumTestGood()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.MinNum(new int[] { 5, 2, 9, 1, 4, 3, 10, 6, 8 }), 1);
        }

        [Test]
        public void MinNumTestBad()
        {
            Assert.AreEqual(WindowsFormsApp2.Class1.MinNum(new int[] { 5, 2, 9, 1, 4, 3, 10, 6, 8 }), 10);
        }



        [Test]
        public void PalindromTestGood()
        {
            Assert.IsTrue(WindowsFormsApp2.Class1.Palindrom("довод"));
        }

        [Test]
        public void PalindromTestBad()
        {
            Assert.IsTrue(WindowsFormsApp2.Class1.Palindrom("Доводы"));
        }

    }
}

