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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StartingWindow : Window
    {
        public StartingWindow()
        {
            InitializeComponent();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DimensionInput.Text, out int dimension))
            {
                MainWindow mainWindow = new MainWindow(dimension);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter a valid number.");
            }
        }
    }


}
