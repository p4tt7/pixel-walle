using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pixel_walle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public MainWindow(int rows, int columns)
        {
            InitializeComponent();
            Rows = rows;
            Columns = columns;
            InitializePixelGrid();

        }

        private void InitializePixelGrid()
        {

            double ladoPixel = Math.Min(727 / Columns, 593 / Rows);

            double cellWidth = ladoPixel * Columns;
            double cellHeight = ladoPixel * Rows;


            PixelGrid.Rows = Rows;
            PixelGrid.Columns = Columns;

            PixelGrid.Width = cellWidth;
            PixelGrid.Height = cellHeight;

            double offsetLeft = 264 + (727 - cellWidth) / 2;
            double offsetTop = 28 + (593 - cellHeight) / 2;

            Canvas.SetLeft(PixelGrid, offsetLeft);
            Canvas.SetTop(PixelGrid, offsetTop);

            for (int i = 0; i < Rows * Columns; i++)
            {
                var pixel = new Border
                {
                    Width = cellWidth,
                    Height = cellHeight,
                    Background = Brushes.White,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0.5),
                };
                PixelGrid.Children.Add(pixel);
            }
        }
        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }




}