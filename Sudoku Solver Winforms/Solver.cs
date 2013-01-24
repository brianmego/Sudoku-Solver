using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    static class Solver
    {
        //byte unsuccessfulRuns = 0;
        //byte unsolvedSlots = 0;

        // Constructor
        //public Solver(List<SudokuSlot> slotList)
        //{
        //    //this.slotList = slotList;
        //    //history.AddSnapshot(slotList);
        //}

        static public void solve(List<SlotCollection> CollectionList, List<SudokuSlot> slotList, SolveHistory history, Queue<SudokuSlot> queueToClean, byte unsuccessfulCounter)
        {
            byte unsuccessfulRuns = unsuccessfulCounter;
            while (CheckForWin(CollectionList, unsuccessfulRuns, 0, slotList) == false)
            {
                foreach(SudokuSlot slot in slotList)
                    RemoveNeighbors(slot, slotList, history, queueToClean);

                foreach (SlotCollection collection in CollectionList)
                {
                    collection.SearchHiddenSingles(history, queueToClean);
                    collection.SearchNakeds(history, queueToClean);
                    collection.SearchHiddenDoubles(history, queueToClean);
                    collection.SearchHiddenTriples(history, queueToClean);
                    collection.SearchHiddenQuads(history, queueToClean);
                }
            }
        }

        // Quits the solve loop if the puzzle is either completely solved or unsolvable for any reason
        static private bool CheckForWin(List<SlotCollection> CollectionList, byte unsuccessfulRuns, byte unsolvedSlots, List<SudokuSlot> slotList)
        {
            if (unsuccessfulRuns <= 3)
            {
                byte tempUnsolvedSlots = 0;
                foreach (SudokuSlot slot in slotList)
                {
                    if (slot.Text == "")
                    {
                        tempUnsolvedSlots++;
                    }
                }
                if (unsolvedSlots == tempUnsolvedSlots)
                {
                    unsuccessfulRuns++;
                    return false;
                }
                else
                {
                    unsolvedSlots = tempUnsolvedSlots;
                    return false;
                }
            }
            else
            {
                if (Solver.IsEmpty(slotList) != true)
                {
                    foreach (SlotCollection collection in CollectionList)
                    {
                        List<string> entryCheckList = new List<string>(9) { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        List<string> overusedEntries = new List<string>();
                        foreach (SudokuSlot slot in collection.paredList)
                        {
                            if (slot.Text != "")
                                if (entryCheckList.Contains(slot.Text))
                                    entryCheckList.Remove(slot.Text);
                                else
                                    overusedEntries.Add(slot.Text);
                            else
                                slot.BackColor = System.Drawing.Color.MintCream;
                        }
                        if (entryCheckList.Count != 0)
                        {
                            foreach (string entry in overusedEntries)
                                foreach (SudokuSlot slot in collection.paredList)
                                {
                                    if (slot.Text == entry)
                                        slot.BackColor = System.Drawing.Color.OrangeRed;
                                }
                        }
                    }
                }
                return true;
            }
        }

        static private bool IsEmpty(List<SudokuSlot> slotList)
        {
            foreach (SudokuSlot slot in slotList)
            {
                if (slot.Text == "")
                    continue;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Delete's a slot's text out of all it's row/column/box's possibleEntries.
        /// </summary>
        /// <param name="senderControl"></param>
        static public void RemoveNeighbors(SudokuSlot senderControl, List<SudokuSlot> slotList, SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            foreach (SudokuSlot slot in slotList)
            {
                if (slot.row == senderControl.row & slot != senderControl)
                {
                    if (slot.RemovePossibleEntry(senderControl.Text) == true)
                        queueToClean.Enqueue(slot);
                }
                else if (slot.column == senderControl.column & slot != senderControl)
                {
                    if (slot.RemovePossibleEntry(senderControl.Text) == true)
                        queueToClean.Enqueue(slot);
                }
                else if (slot.box == senderControl.box & slot != senderControl)
                {
                    if (slot.RemovePossibleEntry(senderControl.Text) == true)
                        queueToClean.Enqueue(slot);
                }
                history.AddSnapshot(slotList);
                
            }
            if (queueToClean.Count > 0)
                RemoveNeighbors(queueToClean.Dequeue(), slotList, history, queueToClean);
        }    
    }
}
