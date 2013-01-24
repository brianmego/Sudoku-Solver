using System.Windows.Controls;
using System.Windows;

namespace Sudoku_Solver.Views
{
    /// <summary>
    /// Interaction logic for SolveGridView.xaml
    /// </summary>
    public partial class SolveGridView : UserControl
    {
        public SolveGridView()
        {
            InitializeComponent();
            DrawGameBoard(9);
        }

        private void DrawGameBoard(int desiredSlots)
        {
            RowDefinition[] rowDefinitions = new RowDefinition[desiredSlots];
            ColumnDefinition[] columnDefinitions = new ColumnDefinition[desiredSlots];

            for (int i = 0; i < desiredSlots; i++)
            {
                rowDefinitions[i] = new RowDefinition();
                columnDefinitions[i] = new ColumnDefinition();

                grdGameBoard.RowDefinitions.Add(rowDefinitions[i]);
                grdGameBoard.ColumnDefinitions.Add(columnDefinitions[i]);
            }
            for (int i = 0; i < grdGameBoard.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grdGameBoard.ColumnDefinitions.Count; j++)
                {
                    var slot = new TextBox();
                    slot.Margin = new Thickness(5);
                    slot.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    slot.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    Grid.SetRow(slot, i);
                    Grid.SetColumn(slot, j);
                    grdGameBoard.Children.Add(slot);
                }
            }
        }
    }
}
