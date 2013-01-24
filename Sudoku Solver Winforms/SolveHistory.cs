using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class SolveHistory
    {
        List<List<SudokuSlot>> history = new List<List<SudokuSlot>>();

        public void AddSnapshot(List<SudokuSlot> slotList)
        {
            history.Add(slotList);
        }

        public void JumpToHist(List<SudokuSlot> slotList, int location)
        {
            slotList = history[location];
        }
    }
    
}
