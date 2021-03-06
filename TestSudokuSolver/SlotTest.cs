﻿using Sudoku_Solver.Models;
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
            Slot target = new Slot(1, 1, 1, "1");
            Assert.IsNotNull(target.AllowedValues, "AllowedValues is Null");
            Assert.IsNotNull(target.Value, "Value is Null");
            Assert.IsNotNull(target.Box, "Box is Null");
            Assert.IsNotNull(target.Column, "Column is Null");
            Assert.IsNotNull(target.Row, "Row is Null");
        }

        /// <summary>
        ///A test for AllowedValues
        ///</summary>
        [TestMethod()]
        public void DefaultAllowedValuesTest()
        {
            Slot target = new Slot(1, 1, 1);
            List<string> expected = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            List<string> actual;
            actual = target.AllowedValues;
            Assert.AreEqual(actual.Except(expected).ToList().Count, 0);
            Assert.AreEqual(expected.Except(actual).ToList().Count, 0);
        }

        /// <summary>
        ///A test for AllowedValues
        ///</summary>
        [TestMethod()]
        public void SpecifiedAllowedValuesTest()
        {
            Slot target = new Slot(1, 1, 1, defaultValues: new List<string>() { "1", "2" });
            List<string> expected = new List<string>() { "1", "2" };
            List<string> actual;
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
            Slot target = new Slot(1, 1, 1);
            int expected = 1;
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
            Slot target = new Slot(1, 1, 1);
            int expected = 1;
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
            Slot target = new Slot(1, 1, 1);
            int expected = 1;
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
            Slot target = new Slot(1, 1, 1);
            string expected = "1";
            string actual;
            target.Value = "1";
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A negative boundary test for Value
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ValueTestNegative()
        {
            Slot target = new Slot(1, 1, 1, "-1");
        }

        /// <summary>
        ///A negative boundary test for Box
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BoxTestNegative()
        {
            Slot target = new Slot(1, 1, -1);
        }

        /// <summary>
        ///A negative boundary test for Column
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ColumnTestNegative()
        {
            Slot target = new Slot(1, -1, 1);
        }

        /// <summary>
        ///A negative boundary test for Row
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RowTestNegative()
        {
            Slot target = new Slot(-1, 1, 1);
        }
    }
}
