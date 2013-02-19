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

        private List<Slot> _slotList = new List<Slot>();
        private List<string> AllowedValues;
        private List<List<Slot>> slotGroupings = new List<List<Slot>>();

        #endregion


        #region Properties

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
            foreach (int allowedValue in SlotList.Select(x => x.Row).AsEnumerable()){
                slotGroupings.Add(SlotList.Where(x => x.Row == allowedValue).ToList());
                slotGroupings.Add(SlotList.Where(x => x.Column == allowedValue).ToList());
                slotGroupings.Add(SlotList.Where(x => x.Box == allowedValue).ToList());
            }
    
        }

        #endregion //Constructor

        
        #region Commands
        public ICommand GenerateEasyPuzzleCommand
        {
            get { return new RelayCommand(GenerateEasyPuzzle); }
        }
        public ICommand GenerateMediumPuzzleCommand
        {
            get { return new RelayCommand(GenerateMediumPuzzle); }
        }
        public ICommand GenerateHardPuzzleCommand
        {
            get { return new RelayCommand(GenerateHardPuzzle); }
        }
        public ICommand GenerateEvilPuzzleCommand
        {
            get { return new RelayCommand(GenerateEvilPuzzle); }
        }
        public ICommand SolvePuzzleCommand
        {
            get { return new RelayCommand(SolvePuzzle); }
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

        public void GenerateEasyPuzzle()
        {
            Clear();
            var slots = WebHarvester.GetEasyPuzzle();
            for (int i = 0; i < slots.Count; i++)
            {
                SlotList[i].Value = slots[i];
            }
        }

        public void GenerateMediumPuzzle()
        {
            Clear();
            var slots = WebHarvester.GetMediumPuzzle();
            for (int i = 0; i < slots.Count; i++)
            {
                SlotList[i].Value = slots[i];
            }
        }
        
        public void GenerateHardPuzzle()
        {
            Clear();
            var slots = WebHarvester.GetHardPuzzle();
            for (int i = 0; i < slots.Count; i++)
            {
                SlotList[i].Value = slots[i];
            }
        }
        
        public void GenerateEvilPuzzle()
        {
            Clear();
            var slots = WebHarvester.GetEvilPuzzle();
            for (int i = 0; i < slots.Count; i++)
            {
                SlotList[i].Value = slots[i];
            }
        }

        /// <summary>
        /// Remove filled in entries as possible entries for all slots sharing a row/column/box
        /// </summary>
        /// <param name="slotGroup">Single group of slots to remove neighbors for</param>
        public static void RemoveNeighbors(List<Slot> slotGroup)
        {
            var filledValues = slotGroup.Select(x => x.Value).ToList();
            var emptySlots = slotGroup.Where(x => x.Value == "").ToList();
            foreach (var slot in emptySlots)
            {
                slot.AllowedValues.RemoveAll(x => filledValues.Contains(x));
            }

        }

        /// <summary>
        /// If a slot has the only instance of a possible entry in its row/column/box, remove
        /// all its other possible entries
        /// </summary>
        /// <param name="group">Single group of slots to solve for</param>
        public static List<Slot> SolveHiddenSingles(List<Slot> slotGroup)
        {
            List<string> allowedValues = new List<string>();
            foreach (List<string> valueGroup in slotGroup.Select(x => x.AllowedValues))
            {
                allowedValues.AddRange(valueGroup);
            }
            foreach (string val in allowedValues.Distinct())
            {
                //If a given entry is only possible once
                if (slotGroup.Count(x => x.AllowedValues.Contains(val)) == 1)
                {
                    //Set it as the value for that Slot
                    slotGroup.First(x => x.AllowedValues.Contains(val)).Value = val;
                }
            }
            return slotGroup;
        }

        /// <summary>
        /// If a subgroup of slots has the only instances of AllowedValues in the group and there are
        /// only enough values to satisfy one value/slot, remove all other AllowedValues from the subgroup
        /// </summary>
        /// <param name="slotGroup">Single group of slots to solve for</param>
        /// <returns>Group of slots with extraneous AllowedValues removed</returns>
        public static List<Slot> SolveHiddens(List<Slot> slotGroup)
        {
            foreach (Slot s in slotGroup.Where(x => (x.AllowedValues.Count > 1)))
            {
                foreach (string val in s.AllowedValues)
                {
                    var slotsWithThisVal = slotGroup.Where(x => x.AllowedValues.Contains(val));
                    slotsWithThisVal.Count();


                    // If x possible Entries are spread across x slots in the collection, remove those slots'
                    // other possibleEntries.
                }
            }

            return slotGroup;
        }

        /// <summary>
        /// If a subgroup of slots has identical AllowedValues and there are only enough slots to 
        /// satisfy one value / slot, remove those AllowedValues from other slots in the group
        /// </summary>
        public static List<Slot> SolveNakeds(List<Slot> slotGroup)
        {
            foreach (Slot s in slotGroup.Where(x=>(x.AllowedValues.Count > 1)))
            {
                var matchingSlots = slotGroup.Where(x => (x.AllowedValues.Count == s.AllowedValues.Count) &&
                                                     (x.AllowedValues.Except(s.AllowedValues).Count() == 0));
                if (matchingSlots.Count() == s.AllowedValues.Count)
                    foreach (Slot slotToPurge in slotGroup.Where(x => !matchingSlots.Contains(x)))
                        slotToPurge.AllowedValues.RemoveAll(x=> s.AllowedValues.Contains(x));
            }
            return slotGroup;
        }

        /// <summary>
        /// Close the program
        /// </summary>
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Set all slots to blank values
        /// </summary>
        public void Clear()
        {
            foreach (Slot s in SlotList)
            {
                s.Value = "";
            }
        }

        /// <summary>
        /// Run the solving functions in the correct order until the puzzle is solved
        /// </summary>
        public void SolvePuzzle()
        {
            int iterations = 0;
            while (SlotList.Count(x => x.Value == "") > 0)
            {
                RemoveAllNeighbors();
                SolveAllHiddenSingles();
                SolveAllNakeds();

                iterations++;
                if (iterations > 10)
                    break;
            }
        }

        /// <summary>
        /// Remove filled in entries as possible entries for all slots sharing a row/column/box
        /// </summary>
        private void RemoveAllNeighbors()
        {
            //For each slotGrouping
            foreach (List<Slot> group in slotGroupings.AsEnumerable())
            {
                RemoveNeighbors(group);
            }
        }

        /// <summary>
        /// If a slot has the only instance of a possible entry in its row/column/box, remove
        /// all its other possible entries
        /// </summary>
        private void SolveAllHiddenSingles()
        {
            foreach (var group in slotGroupings.AsEnumerable())
                SolveHiddenSingles(group);
        }

        private void SolveAllNakeds()
        {
            foreach (var group in slotGroupings.AsEnumerable())
                SolveNakeds(group);
        }

        #endregion
    }
}
