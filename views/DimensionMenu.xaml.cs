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
        public string FileName { get; private set; }


        public DimensionMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileNameBox.Text.Trim();

            if (!IsValidFileName(fileName))
            {
                MessageBox.Show("Nombre inválido. Debe comenzar con una letra y solo puede contener letras, números y guión bajo (_).",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            FileName = fileName;


            if (int.TryParse(RowsBox.Text, out int rows) && int.TryParse(ColsBox.Text, out int cols))
            {
                var mainWindow = new MainWindow(rows, cols, fileName);
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor ingresa dimensiones válidas.");
            }

        }

        private bool IsValidFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (char.IsDigit(name[0]))
                return false;

            foreach (char c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                    return false;
            }

            return true;
        }

    }
}
