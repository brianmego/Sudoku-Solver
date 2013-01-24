using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmSudokuSolver : Form
    {
        // Constructor
        public frmSudokuSolver()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
                if (control is SudokuSlot)
                    allSlots.Add((SudokuSlot)control);



            TestSudoku();
        }

        // Holds references to all SudokuSlots on the form
        private List<SudokuSlot> allSlots = new List<SudokuSlot>();
        
        // Quit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Main Solve button
        private void btnSolve_Click(object sender, EventArgs e)
        {
            foreach(SudokuSlot slot in allSlots)
            {
                slot.LockValues();
                slot.BackColor = System.Drawing.Color.Empty;
            }

            List<SlotCollection> CollectionList = new List<SlotCollection>();
            for (int i = 1; i <= 3; i++)
                for (int j = 1; j <= 9; j++)
                {
                    CollectionList.Add(new SlotCollection(allSlots, j.ToString(), i));
                }

            Queue<SudokuSlot> queueToClean = new Queue<SudokuSlot>();
            SolveHistory history = new SolveHistory();
            Solver.solve(CollectionList, allSlots, history, queueToClean, 0);
        }

        // Reset the form to a blank state
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (SudokuSlot slot in allSlots)
                slot.Reset();
        }

        // Controls movement keys and calling individual slot's Validation method
        private void sudokuSlot_KeyDown(object sender, KeyEventArgs e)
        {
            SudokuSlot senderControl = (SudokuSlot)sender;

            //Navigation
            if (e.KeyCode == Keys.Down)
            {
                foreach (SudokuSlot slot in allSlots)
                    if (senderControl.row == 9)
                    {
                        if (slot.TabIndex == senderControl.TabIndex - 72)
                        {
                            slot.Focus();
                            break;
                        }
                    }
                    else
                        if (slot.TabIndex == senderControl.TabIndex + 9)
                        {
                            slot.Focus();
                            break;
                        }
            }
            else if (e.KeyCode == Keys.Up)
            {
                foreach (SudokuSlot slot in allSlots)
                    if (senderControl.row == 1)
                    {
                        if (slot.TabIndex == senderControl.TabIndex + 72)
                        {
                            slot.Focus();
                            break;
                        }
                    }
                    else
                        if (slot.TabIndex == senderControl.TabIndex - 9)
                        {
                            slot.Focus();
                            break;
                        }
            }
            else if (e.KeyCode == Keys.Right)
            {
                foreach (SudokuSlot slot in allSlots)
                    if (senderControl.row == 9 & senderControl.column == 9)
                    {
                        if (slot.TabIndex == senderControl.TabIndex - 80)
                        {
                            slot.Focus();
                            break;
                        }
                    }
                    else
                        if (slot.TabIndex == senderControl.TabIndex + 1)
                        {
                            slot.Focus();
                            break;
                        }
            }
            else if (e.KeyCode == Keys.Left)
            {
                foreach (SudokuSlot slot in allSlots)
                    if (senderControl.row == 1 & senderControl.column == 1)
                    {
                        if (slot.TabIndex == senderControl.TabIndex + 80)
                        {
                            slot.Focus();
                            break;
                        }
                    }
                    else
                        if (slot.TabIndex == senderControl.TabIndex - 1)
                        {
                            slot.Focus();
                            break;
                        }
            }

            // Send anything else to validation.
            else
            {
                string oldEntry = senderControl.Text;
                senderControl.SudokuSlot_KeyDown(sender, e);
            }   
        }

        // Call slot's specific KeyPress event
        private void sudokuSlot_KeyPress(object sender, KeyPressEventArgs e)
        {
            SudokuSlot senderControl = (SudokuSlot)sender;
            senderControl.SudokuSlot_KeyPress(sender, e);
        }


        // For Testing purposes
        // Gives starting values for all slots
        private void TestSudoku()
        {
            int[] startingValues = new int[81]
            {0,0,0,   8,0,0,   0,0,0,
             0,2,7,   6,0,9,   0,0,0,
             0,0,0,   0,1,0,   0,5,3,

             9,0,0,   2,0,0,   1,0,0,
             0,4,0,   0,0,0,   0,7,0,
             0,0,8,   0,0,5,   0,0,6,

             1,8,0,   0,6,0,   0,0,0,
             0,0,0,   1,0,7,   2,9,0,
             0,0,0,   0,0,8,   0,0,0};

            foreach (SudokuSlot slot in allSlots)
            {
                string value = startingValues[(slot.column + ((slot.row - 1) * 9)) - 1].ToString();
                if (value != "0")
                {
                    slot.Text = value;
                    slot.possibleEntries.Clear();
                }

            }
        }
    }
}
