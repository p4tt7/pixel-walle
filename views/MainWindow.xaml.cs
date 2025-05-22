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
            const double maxWidth = 628;
            const double maxHeight = 527;

            double cellWidth = maxWidth / Columns;
            double cellHeight = maxHeight / Rows;

            double pixelSize = Math.Min(cellWidth, cellHeight);

            double remainingWidth = maxWidth - (Columns * pixelSize);
            double remainingHeight = maxHeight - (Rows * pixelSize);

            PixelGrid.Rows = Rows;
            PixelGrid.Columns = Columns;

            PixelGrid.Width = Columns * pixelSize;
            PixelGrid.Height = Rows * pixelSize;

 
            PixelGrid.Margin = new Thickness(
                remainingWidth / 2,  
                remainingHeight / 2, 
                0,                  
                0                   
            );

            PixelGrid.Children.Clear();

            for (int i = 0; i < Rows * Columns; i++)
            {
                var pixel = new Border
                {
                    Width = pixelSize,
                    Height = pixelSize,
                    Background = Brushes.White,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0.3),
                };
                PixelGrid.Children.Add(pixel);
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }




}