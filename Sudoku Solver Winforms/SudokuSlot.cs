using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class SudokuSlot: System.Windows.Forms.TextBox
    {
        [CategoryAttribute("Layout"), DescriptionAttribute("Row # the slot belongs to. (1-9)")]
            public byte row { get; set; }
        [CategoryAttribute("Layout"), DescriptionAttribute("Column # the slot belongs to. (1-9)")]
            public byte column { get; set; }
        [CategoryAttribute("Layout"), DescriptionAttribute("Box # the slot belongs to. (1-9)")]
            public byte box { get; set; }

        // List of possible entries.  Updates as the program runs.
        public List<string> possibleEntries = new List<string>();
        
        // Used to convert keypresses to their keyboard output
        private Dictionary<int, string> keyValues = new Dictionary<int, string>();

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SudokuSlot
            // 
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SudokuSlot_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SudokuSlot_KeyPress);
            this.ResumeLayout(false);

        }

        // Constructor
        public SudokuSlot()
        {
            PopulateKeyValues(keyValues);
            PopulateEntries(possibleEntries);
        }


        // Fills the possibleEntries List with all starting possible entries (1-9)
        private void PopulateEntries(List<string> possibleEntries)
        {
            for (int i = 1; i <= 9; i++)
                possibleEntries.Add(i.ToString()); // Add 1-9 to possibleEntries
        }

        // Fills the KeyValues dictionary with all possible entries
        private void PopulateKeyValues(Dictionary<int, string> keyValues)
        {
            keyValues.Add(49, "1"); // "1"
            keyValues.Add(50, "2"); // "2"
            keyValues.Add(51, "3"); // "3"
            keyValues.Add(52, "4"); // "4"
            keyValues.Add(53, "5"); // "5"
            keyValues.Add(54, "6"); // "6"
            keyValues.Add(55, "7"); // "7"
            keyValues.Add(56, "8"); // "8"
            keyValues.Add(57, "9");  // "9"
            keyValues.Add(97, "1"); // Numpad "1"
            keyValues.Add(98, "2"); // Numpad "2"
            keyValues.Add(99, "3"); // Numpad "3"
            keyValues.Add(100, "4"); // Numpad "4"
            keyValues.Add(101, "5"); // Numpad "5"
            keyValues.Add(102, "6"); // Numpad "6"
            keyValues.Add(103, "7"); // Numpad "7"
            keyValues.Add(104, "8"); // Numpad "8"
            keyValues.Add(105, "9"); // Numpad "9"
        }

        // Clears the slot's text and restores it's possibleEntries
        public void Reset()
        {
            this.Text = "";
            possibleEntries.Clear();
            PopulateEntries(possibleEntries);
            this.BackColor = System.Drawing.Color.Empty;
        }

        // Handle the KeyDown event to determine the type of character entered into the control.
        public void SudokuSlot_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            IsValidEntry(sender, e); //Validation
        }

        // Determines whether the keystroke is contained in slot's Possible Entries.
        private void IsValidEntry(object sender, KeyEventArgs e)
        {
            SudokuSlot senderControl = (SudokuSlot)sender;

            if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                // If the key is in the slot's possible entries, process it
                try
                {
                    if (this.possibleEntries.Contains(keyValues[e.KeyValue]))
                        this.Text = keyValues[e.KeyValue].ToString();
                }
                catch { senderControl.Text = senderControl.Text; }
            }
        }

        // Cancel keystrokes not allowed by IsValidEntry method.
        public void SudokuSlot_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // If present, will remove a given entry from slot's PossibleEntries
        public bool RemovePossibleEntry(string possEntryToDelete)
        {
            if (possEntryToDelete != "")
                if (possibleEntries.Contains(possEntryToDelete))
                    this.possibleEntries.Remove(possEntryToDelete);
            if (possibleEntries.Count == 1)
            {
                SetToText();
                return true;
            }
            return false;
        }

        // If the slot has only one possible entry, set it's text to that entry.
        public void SetToText()
        {
            if (this.possibleEntries.Count == 1)
            {
                this.Text = this.possibleEntries[0];
                this.possibleEntries.Clear();
                this.Update();

                System.Threading.Thread.Sleep(20);
            }
        }

        // Removes all other possible entries other than the one entered
        public void LockValues()
        {
            if (this.Text != "")
                this.possibleEntries.Clear();
        }  
    }
}
