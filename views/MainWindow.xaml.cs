using pixel_walle.controllers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using pixel_walle.controllers;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Resources;
using System.IO.Enumeration;

namespace pixel_walle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private SourceReader reader = new SourceReader();
        private Error_Manager errorManager = new Error_Manager();
        public string FileName { get; private set; }
        private Manager manager;


        public MainWindow(int rows, int columns, string fileName)
        {
            InitializeComponent();

            Rows = rows;
            Columns = columns;
            FileName = fileName;

            InitializePixelGrid();

            manager = new Manager(fileName, "", Columns, Rows);
            var renderer = new CanvasRender(PixelGrid, manager.Context);
            renderer.Render();


        }

        private void InitializePixelGrid()
        {
            const double maxWidth = 653;
            const double maxHeight = 527;
            const double rectLeft = 480;
            const double rectTop = 77;

            double cellWidth = maxWidth / Columns;
            double cellHeight = maxHeight / Rows;

            double pixelSize = Math.Min(cellWidth, cellHeight);

            double gridWidth = Columns * pixelSize;
            double gridHeight = Rows * pixelSize;

            double offsetX = (maxWidth - gridWidth) / 2;
            double offsetY = (maxHeight - gridHeight) / 2;

            Canvas.SetLeft(PixelGrid, rectLeft + offsetX);
            Canvas.SetTop(PixelGrid, rectTop + offsetY);

            PixelGrid.Width = gridWidth;
            PixelGrid.Height = gridHeight;

            PixelGrid.Rows = Rows;
            PixelGrid.Columns = Columns;

            PixelGrid.Children.Clear();
        }

        private void ErrorsButton_Click(object sender, RoutedEventArgs e)
        {
            if (errorManager.IsEmpty())
            {
                MessageBox.Show("No hay errores que mostrar.", "Sin errores", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            errorManager.ShowErrorWindow();
        }


        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileName;
            string codigoFuente = miTextBox.Text;
            

            errorManager.Clear();

            manager = new Manager(fileName, codigoFuente, Columns, Rows);
            manager.Analyze();

            if (manager.Errors.Count > 0)
            {

                foreach (var err in manager.Errors)
                {
                    errorManager.Add(err); 
                }

                MessageBox.Show($"No se pudo compilar. Hay {manager.Errors.Count} errores.", "Errores", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var renderer = new CanvasRender(PixelGrid, manager.Context);
                renderer.Render();

                MessageBox.Show("Compilación exitosa.", "Compilado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void ClearButton_Click()
        {
            miTextBox.Clear();

        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PixelWalle Files (*.pw)|*.pw",
                Title = "Guardar archivo",
                DefaultExt = "pw"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(dialog.FileName, miTextBox.Text);
                    MessageBox.Show("Archivo guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void RedimensionButton_Click(object sender, RoutedEventArgs e)
        {
            PixelGrid.Children.Clear();
            InitializePixelGrid();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }




}