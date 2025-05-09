﻿using System;
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
using Microsoft.Win32;


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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pixel Walle Project (*.pwe)|*.pwe";
            openFileDialog.Title = "Cargar Proyecto";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                MessageBox.Show($"Archivo seleccionado: {filePath}");

            }
        }



    }

}



