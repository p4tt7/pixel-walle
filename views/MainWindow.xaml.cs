using pixel_walle.controllers;
using System.Reflection.PortableExecutable;
using pixel_walle.src.Errors;
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
using System.Text.Json;
using pixel_walle.src.AST;

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
            const double maxWidth = 402;   
            const double maxHeight = 400;  
            const double rectLeft = 430;  
            const double rectTop = 54;     


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

            manager = new Manager(fileName, codigoFuente, this.Columns, this.Rows);
            bool compiled = manager.Compile(out PixelWalleProgram? program);

            var renderer = new CanvasRender(PixelGrid, manager.Context);
            renderer.Render();

            if (!compiled)
            {
                foreach (var err in manager.Errors)
                    errorManager.Add(err);

                MessageBox.Show(
                    $"Compilation failed with {manager.Errors.Count} error(s).",
                    "Compilation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            List<Error> runtimeErrors = new List<Error>();

            while (!program.IsFinished && runtimeErrors.Count == 0)
            {
                bool success = program.ExecuteNextInstruction(manager.Context, runtimeErrors);
                renderer.Render();

                if (!success || runtimeErrors.Count>0)
                    break;
            }

            if(runtimeErrors.Count > 0)
            {
                foreach (var err in runtimeErrors)
                    errorManager.Add(err);

                MessageBox.Show(
                    $"Execution stopped due to {runtimeErrors.Count} runtime error(s).",
                    "Runtime Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }

            else
            {
                MessageBox.Show(
                    "Program executed successfully.",
                    "Execution Finished",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }



        }



        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            miTextBox.Clear();
             
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {


            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = FileName,
                Filter = "PixelWalle Files (*.pw)|*.pw",
                Title = "Save file",
                DefaultExt = "pw",
                AddExtension = true
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var project = new PixelWalleProject
                    {
                        CodeText = miTextBox.Text,
                        CanvasWidth = Rows,
                        CanvasHeight = Columns 
                    };

                    var json = JsonSerializer.Serialize(project);
                    File.WriteAllText(dialog.FileName, json);

                    MessageBox.Show("Project saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Error while saving the project:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
        }


        private void RedimensionButton_Click(object sender, RoutedEventArgs e)
        {
            var resizeDialog = new ResizeDialog(Rows, Columns);
            if (resizeDialog.ShowDialog() == true)
            {
                this.Rows = resizeDialog.Rows;
                this.Columns = resizeDialog.Columns;

                this.manager = new Manager(FileName, miTextBox.Text, this.Columns, this.Rows);
                InitializePixelGrid();
                var renderer = new CanvasRender(PixelGrid, manager.Context);
                renderer.Render();
            }
        }









        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }




}