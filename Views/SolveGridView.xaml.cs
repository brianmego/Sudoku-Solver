using System.Windows.Controls;
using System.Windows;
using Sudoku_Solver.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Sudoku_Solver.Views
{
    /// <summary>
    /// Interaction logic for SolveGridView.xaml
    /// </summary>
    public partial class SolveGridView : UserControl
    {
        SolveGridViewModel ViewModel = new SolveGridViewModel();
        public SolveGridView()
        {
            InitializeComponent();
            //Gotta set the viewmodel in the codebehind because my brain won't give me any other options for dynamically setting the size of the board, sorry.
            this.DataContext = ViewModel;

            DrawGameBoard(ViewModel.SlotList);

            
        }

        private void DrawGameBoard(List<Models.Slot> slotList)
        {
            int desiredSlots = (int)Math.Sqrt(slotList.Count);
            RowDefinition[] rowDefinitions = new RowDefinition[desiredSlots];
            ColumnDefinition[] columnDefinitions = new ColumnDefinition[desiredSlots];

            for (int i = 0; i < desiredSlots; i++)
            {
                rowDefinitions[i] = new RowDefinition();
                columnDefinitions[i] = new ColumnDefinition();

                grdGameBoard.RowDefinitions.Add(rowDefinitions[i]);
                grdGameBoard.ColumnDefinitions.Add(columnDefinitions[i]);
            }
            foreach (Models.Slot slot in slotList)
            {
                var textBox = new TextBox();
                textBox.Margin = new Thickness(5);
                textBox.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                textBox.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                Grid.SetRow(textBox, slot.Row);
                Grid.SetColumn(textBox, slot.Column);
                Binding myBinding = new Binding("Value");
                myBinding.Source = slot;
                textBox.SetBinding(TextBox.TextProperty, myBinding);
                grdGameBoard.Children.Add(textBox);
            }
        }
    }
}
