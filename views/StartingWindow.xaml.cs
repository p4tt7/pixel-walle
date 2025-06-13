using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using pixel_walle.controllers;


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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void New_Project(object sender, RoutedEventArgs e)
        {
            DimensionMenu ventana = new DimensionMenu();
            ventana.Show();
            this.Close();

        }    
    

    private void Load_Project(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pixel Walle Project (*.pw)|*.pw",
                Title = "Cargar Proyecto"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    PixelWalleProject project = JsonSerializer.Deserialize<PixelWalleProject>(json);

                    if (project != null)
                    {
                     
                        var mainWindow = new MainWindow(project.CanvasHeight, project.CanvasWidth, openFileDialog.FileName);

                 
                        mainWindow.Show();

                
                        mainWindow.miTextBox.Text = project.CodeText;


                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El archivo está vacío o dañado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el proyecto:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }




}



