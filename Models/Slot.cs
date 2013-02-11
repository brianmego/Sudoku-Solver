﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acme.Common.Infrastructure;

namespace Sudoku_Solver.Models
{
    class Slot : ObservableObject
    {
        #region Declarations

        private int _row;
        private int _column;
        private int _box;
        private string _value;
        private List<string> _allowedValues;

        #endregion //Declarations


        #region Properties

        public int Row
        {
            get { return _row; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _row = value;
                RaisePropertyChanged("Row");
            }
        }
        public int Column
        {
            get { return _column; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _column = value;
                RaisePropertyChanged("Column");
            }
        }
        public int Box
        {
            get { return _box; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _box = value;
                RaisePropertyChanged("Box");
            }
        }
        public string Value
        {
            get { return _value; }
            set
            {
                if (!AllowedValues.Contains(value))
                {
                    throw new ArgumentException("Provided value not a valid option");
                }
                _value = value;
                if (value != "")
                    AllowedValues.RemoveAll(x => (x != value && x != ""));
                RaisePropertyChanged("Value");
            }
        }
        public List<string> AllowedValues
        {
            get { return _allowedValues; }
            set
            {
                _allowedValues = value;
                RaisePropertyChanged("AllowedValues");
                if (AllowedValues.Count == 2)
                {
                    this.Value = AllowedValues[0]; //Only Non-blank left
                }
            }
        }

        #endregion //Properties


        #region Constructor

        public Slot(int row, int column, int box, string value="")
        {
            AllowedValues = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "" };

            Row = row;
            Column = column;
            Box = box;
            Value = value;
        }

        #endregion
    }
}
