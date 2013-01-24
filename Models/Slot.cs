using System;
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
        private int _value;
        private List<int> _allowedValues;

        #endregion //Declarations


        #region Properties

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                RaisePropertyChanged("Row");
            }
        }
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                RaisePropertyChanged("Column");
            }
        }
        public int Box
        {
            get { return _box; }
            set
            {
                _box = value;
                RaisePropertyChanged("Box");
            }
        }
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }
        public List<int> AllowedValues
        {
            get { return _allowedValues; }
            set
            {
                _allowedValues = value;
                RaisePropertyChanged("AllowedValues");
            }
        }

        #endregion //Properties

    }
}
