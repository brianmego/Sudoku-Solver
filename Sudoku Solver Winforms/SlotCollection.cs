using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class SlotCollection
    {
        private byte row = 0;
        private byte column = 0;
        private byte box = 0;
        public List<SudokuSlot> paredList = new List<SudokuSlot>();
        private List<string> collPossibleEntries = new List<string>();
        private List<SudokuSlot> allSlots = new List<SudokuSlot>();

        // Constructor
        /// <summary>
        /// Holds references to each slot in a given row/column/box
        /// </summary>
        /// <param name="slotList">
        /// Slots the collection will be formed from
        /// </param> 
        /// <param name="stringToSearch">
        /// The row/column/box the collection will search for
        /// </param>
        /// <param name="organization">
        /// 1 for row, 2 for column, 3 for box
        /// </param>
        public SlotCollection(List<SudokuSlot> slotList, string stringToSearch, int organization)
        {
            foreach (SudokuSlot slot in slotList)
            {
                allSlots = slotList;
                int[] organizations = new int[3] { slot.row, slot.column, slot.box };
                if (organizations[organization - 1].ToString() == stringToSearch)
                    paredList.Add(slot);
                
                if (organization == 1)
                    this.row = Convert.ToByte(stringToSearch);
                else if (organization == 2)
                    this.column = Convert.ToByte(stringToSearch);
                else if (organization == 3)
                    this.box = Convert.ToByte(stringToSearch);
            }

        }

        // Brings the collection's possibleEntries list up do date with the slots it contains
        private void UpdatePossibleEntries()
        {
            foreach (SudokuSlot slot in paredList)
                foreach (string possEntry in slot.possibleEntries)
                    if (!this.collPossibleEntries.Contains(possEntry))
                        this.collPossibleEntries.Add(possEntry);
        }
        
        // If a slot has the only instance of an PossibleEntry in its row/column/box, remove
        // all its other possible entries
        public void SearchHiddenSingles(SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            for (int entry = 1; entry <= 9; entry++)
            {
                int counter = 0;
                foreach (SudokuSlot slot in paredList)
                {
                    if (slot.possibleEntries.Contains(entry.ToString()))
                        counter++;
                    if (counter > 1)
                        break;
                }

                if (counter == 1)
                {
                    foreach (SudokuSlot slot in paredList)
                        if (slot.possibleEntries.Contains(entry.ToString()))
                        {
                            for (int i = 1; i<=9;i++)
                                if (slot.possibleEntries.Contains(i.ToString()))
                                    if (i != entry)
                                    {
                                        if (slot.RemovePossibleEntry(i.ToString()) == true)
                                        {
                                            Solver.RemoveNeighbors(slot, allSlots, history, queueToClean);
                                            
                                        }
                                    }
                        }
                }
            }
        }

        // Find slots with identical possibleEntries and remove them from
        // other slots
        public void SearchNakeds(SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            byte counter = 0;
            foreach (SudokuSlot slot in paredList)
            {
                foreach (SudokuSlot slot_2 in paredList)
                {
                    if (slot_2.possibleEntries == slot.possibleEntries && slot_2 != slot)
                        counter++;
                }
                if (counter == slot.possibleEntries.Count)
                {
                    foreach (SudokuSlot slot_2 in paredList)
                        if (slot_2.possibleEntries != slot.possibleEntries)
                            foreach (string entry in slot.possibleEntries)
                            {
                                if (slot_2.RemovePossibleEntry(entry) == true)
                                    Solver.RemoveNeighbors(slot, allSlots, history, queueToClean);
                            }
                }
            }
        }

        // If 2 possible Entries are spread across 2 slots in the collection, remove those slots'
        // other possibleEntries.
        public void SearchHiddenDoubles(SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            this.UpdatePossibleEntries();

            foreach (string entry in this.collPossibleEntries)
            {
                foreach (string entry_2 in this.collPossibleEntries)
                {
                    List<SudokuSlot> slotsWithPossHiddens = new List<SudokuSlot>();
                    if (entry_2 != entry)
                        foreach (SudokuSlot slot in this.paredList)
                            if (slot.possibleEntries.Contains(entry) || slot.possibleEntries.Contains(entry_2))
                                if (!slotsWithPossHiddens.Contains(slot))
                                {
                                    slotsWithPossHiddens.Add(slot);
                                    if (slotsWithPossHiddens.Count > 2)
                                        break; // If spread across more than 2 slots, save CPU cycles
                                }
                    if (slotsWithPossHiddens.Count == 2)
                    {
                        List<string> allPossibles = new List<string>();
                        foreach (SudokuSlot slot in slotsWithPossHiddens)
                        {
                            foreach (string possEntry in slot.possibleEntries)
                                allPossibles.Add(possEntry);
                        }

                        if (allPossibles.Contains(entry) && allPossibles.Contains(entry_2))
                        {
                            foreach (SudokuSlot slot in slotsWithPossHiddens)
                            {
                                if (!(slot.possibleEntries.Count == 2 &&
                                    slot.possibleEntries.Contains(entry) &&
                                    slot.possibleEntries.Contains(entry_2)))
                                {
                                    List<string> tempPossibleEntries = new List<string>();
                                    foreach (string possEntry in slot.possibleEntries)
                                        tempPossibleEntries.Add(possEntry);

                                    foreach (string possEntry in tempPossibleEntries)
                                        if (possEntry != entry && possEntry != entry_2)
                                        {
                                            if (slot.RemovePossibleEntry(possEntry) == true)
                                                Solver.RemoveNeighbors(slot, allSlots, history, queueToClean);
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }

        // If 3 possible Entries are spread across 3 slots in the collection, remove those slots'
        // other possibleEntries.
        public void SearchHiddenTriples(SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            this.UpdatePossibleEntries();

            foreach (string entry in this.collPossibleEntries)
            {
                foreach (string entry_2 in this.collPossibleEntries)
                {
                    foreach (string entry_3 in this.collPossibleEntries)
                    {
                        List<SudokuSlot> slotsWithPossHiddens = new List<SudokuSlot>();
                        if (entry_3 != entry_2 && entry_3 != entry && entry_2 != entry)
                            foreach (SudokuSlot slot in this.paredList)
                                if (slot.possibleEntries.Contains(entry) || 
                                    slot.possibleEntries.Contains(entry_2) || 
                                    slot.possibleEntries.Contains(entry_3))
                                    if (!slotsWithPossHiddens.Contains(slot))
                                    {
                                        slotsWithPossHiddens.Add(slot);
                                        if (slotsWithPossHiddens.Count > 3)
                                            break; // If spread across more than 2 slots, save CPU cycles
                                    }
                        if (slotsWithPossHiddens.Count == 3)
                        {
                            List<string> allPossibles = new List<string>();
                            foreach (SudokuSlot slot in slotsWithPossHiddens)
                            {
                                foreach (string possEntry in slot.possibleEntries)
                                    allPossibles.Add(possEntry);
                            }

                            if (allPossibles.Contains(entry) && allPossibles.Contains(entry_2) && allPossibles.Contains(entry_3))
                            {
                                foreach (SudokuSlot slot in slotsWithPossHiddens)
                                {
                                    if (!(slot.possibleEntries.Count == 3 &&
                                        slot.possibleEntries.Contains(entry) &&
                                        slot.possibleEntries.Contains(entry_2) &&
                                        slot.possibleEntries.Contains(entry_3)))
                                    {
                                        List<string> tempPossibleEntries = new List<string>();
                                        foreach (string possEntry in slot.possibleEntries)
                                            tempPossibleEntries.Add(possEntry);

                                        foreach (string possEntry in tempPossibleEntries)
                                            if (possEntry != entry && possEntry != entry_2 && possEntry != entry_3)
                                            {
                                                if (slot.RemovePossibleEntry(possEntry) == true)
                                                    Solver.RemoveNeighbors(slot, allSlots, history, queueToClean);
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // If 4 possible Entries are spread across 4 slots in the collection, remove those slots'
        // other possibleEntries.
        public void SearchHiddenQuads(SolveHistory history, Queue<SudokuSlot> queueToClean)
        {
            this.UpdatePossibleEntries();

            foreach (string entry in this.collPossibleEntries)
            {
                foreach (string entry_2 in this.collPossibleEntries)
                {
                    foreach (string entry_3 in this.collPossibleEntries)
                    {
                        foreach (string entry_4 in this.collPossibleEntries)
                        {
                            List<SudokuSlot> slotsWithPossHiddens = new List<SudokuSlot>();
                            if (entry_4 != entry_3 &&
                                entry_4 != entry_2 &&
                                entry_4 != entry &&
                                entry_3 != entry_2 && 
                                entry_3 != entry && 
                                entry_2 != entry)
                                foreach (SudokuSlot slot in this.paredList)
                                    if (slot.possibleEntries.Contains(entry) ||
                                        slot.possibleEntries.Contains(entry_2) ||
                                        slot.possibleEntries.Contains(entry_3) ||
                                        slot.possibleEntries.Contains(entry_4))
                                        if (!slotsWithPossHiddens.Contains(slot))
                                        {
                                            slotsWithPossHiddens.Add(slot);
                                            if (slotsWithPossHiddens.Count > 4)
                                                break; // If spread across more than 2 slots, save CPU cycles
                                        }
                            if (slotsWithPossHiddens.Count == 4)
                            {
                                List<string> allPossibles = new List<string>();
                                foreach (SudokuSlot slot in slotsWithPossHiddens)
                                {
                                    foreach (string possEntry in slot.possibleEntries)
                                        allPossibles.Add(possEntry);
                                }

                                if (allPossibles.Contains(entry) && 
                                    allPossibles.Contains(entry_2) && 
                                    allPossibles.Contains(entry_3) &&
                                    allPossibles.Contains(entry_4))
                                {
                                    foreach (SudokuSlot slot in slotsWithPossHiddens)
                                    {
                                        if (!(slot.possibleEntries.Count == 4 &&
                                            slot.possibleEntries.Contains(entry) &&
                                            slot.possibleEntries.Contains(entry_2) &&
                                            slot.possibleEntries.Contains(entry_3) &&
                                            slot.possibleEntries.Contains(entry_4)))
                                        {
                                            List<string> tempPossibleEntries = new List<string>();
                                            foreach (string possEntry in slot.possibleEntries)
                                                tempPossibleEntries.Add(possEntry);

                                            foreach (string possEntry in tempPossibleEntries)
                                                if (possEntry != entry && 
                                                    possEntry != entry_2 &&
                                                    possEntry != entry_3 &&
                                                    possEntry != entry_4)
                                                {
                                                    if (slot.RemovePossibleEntry(possEntry) == true)
                                                        Solver.RemoveNeighbors(slot, allSlots, history, queueToClean);
                                                }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
