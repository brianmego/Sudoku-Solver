using Sudoku_Solver.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSudokuSolver
{
    
    
    /// <summary>
    ///This is a test class for SlotTest and is intended
    ///to contain all SlotTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SlotTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Slot Constructor
        ///</summary>
        [TestMethod()]
        public void SlotConstructorTest()
        {
            Slot target = new Slot();
            Assert.IsNotNull(target.AllowedValues);
            Assert.IsNotNull(target.Value);
            Assert.IsNotNull(target.Box);
            Assert.IsNotNull(target.Column);
            Assert.IsNotNull(target.Row);
        }

        /// <summary>
        ///A test for AllowedValues
        ///</summary>
        [TestMethod()]
        public void AllowedValuesTest()
        {
            Slot target = new Slot();
            List<int> expected = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> actual;
            actual = target.AllowedValues;
            Assert.AreEqual(actual.Except(expected).ToList().Count, 0);
            Assert.AreEqual(expected.Except(actual).ToList().Count, 0);
        }

        /// <summary>
        ///A test for Box
        ///</summary>
        [TestMethod()]
        public void BoxTest()
        {
            Slot target = new Slot();
            int expected = 0;
            int actual;
            target.Box = expected;
            actual = target.Box;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Column
        ///</summary>
        [TestMethod()]
        public void ColumnTest()
        {
            Slot target = new Slot();
            int expected = 0;
            int actual;
            target.Column = expected;
            actual = target.Column;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Row
        ///</summary>
        [TestMethod()]
        public void RowTest()
        {
            Slot target = new Slot();
            int expected = 0;
            int actual;
            target.Row = expected;
            actual = target.Row;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            Slot target = new Slot();
            int expected = 0;
            int actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A negative boundary test for Value
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValueTestNegative()
        {
            Slot target = new Slot();
            int input = -1;
            target.Value = input;
        }

        /// <summary>
        ///A negative boundary test for Box
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BoxTestNegative()
        {
            Slot target = new Slot();
            int input = -1;
            target.Box = input;
        }

        /// <summary>
        ///A negative boundary test for Column
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ColumnTestNegative()
        {
            Slot target = new Slot();
            int input = -1;
            target.Column = input;
        }

        /// <summary>
        ///A negative boundary test for Row
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RowTestNegative()
        {
            Slot target = new Slot();
            int input = -1;
            target.Row = input;
        }
    }
}
