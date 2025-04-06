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
        public MainWindow()
        {
            InitializeComponent();
            InitializePixelGrid();
 
        }

        private void InitializePixelGrid()
        {

            for (int i = 0; i < 64; i++) 
            {
                var pixel = new Border
                {
                    Background=Brushes.White,
                    BorderBrush=Brushes.Black,
                    BorderThickness=new Thickness(1),
                };
                PixelGrid.Children.Add(pixel);
            }
        }
    }



}