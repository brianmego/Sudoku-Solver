using Sudoku_Solver.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sudoku_Solver.Models;
using System.Collections.Generic;

namespace TestSudokuSolver
{
    
    
    /// <summary>
    ///This is a test class for SolveGridViewModelTest and is intended
    ///to contain all SolveGridViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SolveGridViewModelTest
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
        ///A test for SolveGridViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void SolveGridViewModelConstructorTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SlotList
        ///</summary>
        [TestMethod()]
        public void SlotListTest()
        {
            SolveGridViewModel target = new SolveGridViewModel(); // TODO: Initialize to an appropriate value
            List<Slot> expected = null; // TODO: Initialize to an appropriate value
            List<Slot> actual;
            target.SlotList = expected;
            actual = target.SlotList;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SolveGridHeight
        ///</summary>
        [TestMethod()]
        public void SolveGridHeightTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            int expected = 0;
            int actual;
            target.SolveGridHeight = expected;
            actual = target.SolveGridHeight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SolveGridWidth
        ///</summary>
        [TestMethod()]
        public void SolveGridWidthTest()
        {
            SolveGridViewModel target = new SolveGridViewModel(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.SolveGridWidth = expected;
            actual = target.SolveGridWidth;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
