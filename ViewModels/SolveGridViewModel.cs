using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acme.Common.Infrastructure;
using Sudoku_Solver.Models;

namespace Sudoku_Solver.ViewModels
{
    class SolveGridViewModel : ObservableObject
    {
        #region Declarations

        private int _solveGridHeight = 400;
        private List<Slot> _slotList = new List<Slot>();

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
            for (double i = 1; i <= desiredSlots; i++)
            {
                for (double j = 1; j <= desiredSlots; j++)
                {
                    Slot slot = new Slot()
                    {
                        Row = (int)i,
                        Column = (int)j
                    };
                    double Box = Math.Floor((i - 1) / Math.Sqrt(desiredSlots));
                    Box *= Math.Sqrt(desiredSlots);
                    Box += Math.Ceiling(j / Math.Sqrt(desiredSlots));
                    slot.Box = (int)Box;
                    SlotList.Add(slot);
                }
            }

            var wh = new Models.WebHarvester();
        }

        #endregion //Constructor
    }
}
