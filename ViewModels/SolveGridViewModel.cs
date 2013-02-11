using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acme.Common.Infrastructure;
using Sudoku_Solver.Models;
using System.Windows.Input;
using System.Windows;

namespace Sudoku_Solver.ViewModels
{
    class SolveGridViewModel : ObservableObject
    {
        #region Declarations

        private int _solveGridHeight = 400;
        private List<Slot> _slotList = new List<Slot>();
        private List<string> AllowedValues;
        private List<IGrouping<int, Slot>> slotGroupings = new List<IGrouping<int,Slot>>();

        #endregion


        #region Properties

        public int SolveGridHeight
        {
            get { return _solveGridHeight; }
            set
            {
                _solveGridHeight = value;
                RaisePropertyChanged("SolveGridHeight");
            }
        }

        public int SolveGridWidth
        {
            get { return _solveGridHeight; }
            set
            {
                SolveGridHeight = value;
                RaisePropertyChanged("SolveGridWidth");
            }
        }

        public List<Slot> SlotList
        {
            get { return _slotList; }
            set
            {
                _slotList = value;
                RaisePropertyChanged("SlotList");
            }
        }

        #endregion //Properties


        #region Constructor

        public SolveGridViewModel()
        {
            int desiredSlots = 9;
            for (int i = 1; i <= desiredSlots; i++)
            {
                for (int j = 1; j <= desiredSlots; j++)
                {
                    int row = i;
                    int column = j;
                    double box = Math.Floor((i - 1) / Math.Sqrt(desiredSlots));
                    box *= Math.Sqrt(desiredSlots);
                    box += Math.Ceiling(j / Math.Sqrt(desiredSlots));
                    SlotList.Add(new Slot(row, column, (int)box));
                }
            }

            AllowedValues = new List<string>(SlotList[0].AllowedValues);
            slotGroupings.AddRange(SlotList.GroupBy(x => x.Row).ToList());
            slotGroupings.AddRange(SlotList.GroupBy(x => x.Column).ToList());
            slotGroupings.AddRange(SlotList.GroupBy(x => x.Box).ToList());       
        }

        #endregion //Constructor

        
        #region Commands
        public ICommand GeneratePuzzleCommand
        {
            get { return new RelayCommand(GeneratePuzzle); }
        }
        public ICommand SolvePuzzleCommand
        {
            get { return new RelayCommand(SolvePuzzle); }
        }
        public ICommand SolveHiddenSinglesCommand
        {
            get { return new RelayCommand(SolveHiddenSingles); }
        }
        public ICommand ClearCommand
        {
            get { return new RelayCommand(Clear); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(Exit); }
        }
        #endregion


        #region Methods

        public void GeneratePuzzle()
        {
            var wh = new Models.WebHarvester();
            var slots = wh.GetSamplePuzzle();
            for (int i = 0; i < slots.Count; i++)
            {
                SlotList[i].Value = slots[i];
            }
        }

        /// <summary>
        /// Remove filled in entries as possible entries for all slots sharing a row/column/box
        /// </summary>
        /// <param name="group">Single group of slots to remove neighbors for</param>
        public void RemoveNeighbors(IGrouping<int,Slot> group)
        {
            //get the list of values that have been found in that group
            //for each empty slot in that group
            //remove all the values that have been filled from possibilities
            var filledValues = group.Select(x => x.Value).ToList();
            var emptySlots = group.Where(x => x.Value == "").ToList();
            foreach (var slot in emptySlots)
            {
                slot.AllowedValues.RemoveAll(x => filledValues.Contains(x));
            }
        }

        /// <summary>
        /// Remove filled in entries as possible entries for all slots sharing a row/column/box
        /// </summary>
        private void RemoveAllNeighbors()
        {
            //For each slotGrouping
            foreach (IGrouping<int, Slot> group in slotGroupings.AsEnumerable())
            {
                RemoveNeighbors(group);
            }
        }

        /// <summary>
        /// If a slot has the only instance of a possible entry in its row/column/box, remove
        /// all its other possible entries
        /// </summary>
        public void SolveHiddenSingles()
        {
            //For each slotGrouping
            foreach (var group in slotGroupings.AsEnumerable())
            {
                foreach (string val in AllowedValues)
                {
                    //If a given entry is only possible once
                    if (group.Count(x => x.AllowedValues.Contains(val)) == 1)
                    {
                        //Set it as the value for that Slot
                        group.First(x => x.AllowedValues.Contains(val)).Value = val;
                    }
                }
            }
        }

        public void SolveNakeds()
        {

        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Clear()
        {
            foreach (Slot s in SlotList)
            {
                s.Value = "";
            }
        }

        public void SolvePuzzle()
        {
            SolveHiddenSingles();
            RemoveAllNeighbors();
            throw new NotImplementedException();
        }

        #endregion
    }
}
