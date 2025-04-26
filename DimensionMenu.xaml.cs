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
    /// Interaction logic for DimensionMenu.xaml
    /// </summary>
    public partial class DimensionMenu : Window
    {

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public DimensionMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(RowsBox.Text, out int rows) && int.TryParse(ColsBox.Text, out int cols))
            {
                var mainWindow = new MainWindow(rows, cols);
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor ingresa dimensiones válidas.");
            }

        }
    }
}
