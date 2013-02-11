using Sudoku_Solver.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sudoku_Solver.Models;
using System.Collections.Generic;
using System.Linq;

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
        }

        /// <summary>
        ///A test for SlotList
        ///</summary>
        [TestMethod()]
        public void SlotListTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            int expected = 81;
            int actual;
            actual = target.SlotList.Count;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SolveGridHeight
        ///</summary>
        [TestMethod()]
        public void SolveGridHeightTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            int expected = 450;
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
            SolveGridViewModel target = new SolveGridViewModel();
            int expected = 450;
            int actual;
            target.SolveGridWidth = expected;
            actual = target.SolveGridWidth;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSamplePuzzle
        ///</summary>
        [TestMethod()]
        public void GetSamplePuzzleTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            target.GeneratePuzzle();
            bool expected = false;
            foreach (var slot in target.SlotList)
            {
                if (slot.Value != "")
                    expected = true;
            }
            if (expected==false)
            {
                Assert.Fail("SlotList was not populated");
            }
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ClearTest()
        {
            SolveGridViewModel target = new SolveGridViewModel();
            target.SlotList.Add(new Slot(1, 1, 1, "3"));
            target.Clear();

            int expected = 0;
            int actual = 0;
            foreach (Slot s in target.SlotList)
            {
                if (s.Value != "")
                    actual++;
            }
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RemoveNeighbors
        ///</summary>
        [TestMethod()]
        public void RemoveNeighborsTest()
        {
            List<IGrouping<int, Slot>> slotGroupings = new List<IGrouping<int,Slot>>();
            SolveGridViewModel target = new SolveGridViewModel();
            Slot slotA = new Slot(1, 1, 1);
            Slot slotB = new Slot(1, 2, 1, "1");
            List<Slot> SlotList = new List<Slot>() { slotA, slotB };
            
            slotGroupings.AddRange(SlotList.GroupBy(x => x.Row).ToList());
            target.RemoveNeighbors(slotGroupings[0]);
            Assert.IsFalse(slotA.AllowedValues.Contains("1"));
        }
    }
}
