using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pixel_walle
{
    /// <summary>
    /// Interaction logic for ResizeDialog.xaml
    /// </summary>
    public partial class ResizeDialog : Window
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public ResizeDialog(int currentRows, int currentColumns)
        {
            InitializeComponent();
            RowsBox.Text = currentRows.ToString();
            ColumnsBox.Text = currentColumns.ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RowsBox.Text, out int rows) && rows > 0 &&
                int.TryParse(ColumnsBox.Text, out int columns) && columns > 0)
            {
                Rows = rows;
                Columns = columns;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter valid positive integers.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}
