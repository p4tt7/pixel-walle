using System;
using System.Collections.Generic;
using pixel_walle.src.Errors;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace pixel_walle.controllers
{
    public class Error_Manager
    {
        private readonly List<Error> errors = new List<Error>();
        private TextBox? errorDisplay;

        public void Clear()
        {
            errors.Clear();
            UpdateDisplay();
        }
        public void Add(Error error)
        {
            errors.Add(error);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (errorDisplay == null) return;

            var sb = new StringBuilder();
            foreach (var err in errors)
            {
                sb.AppendLine(err.ToString());
            }

            errorDisplay.Text = sb.ToString();
        }

        public void ShowErrorWindow()
        {
            if (errors.Count == 0)
            {
                MessageBox.Show("No errors to show.", "No errors", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var ventanaErrores = new ErrorWindow(errors);
            ventanaErrores.ShowDialog(); 
        }

        public bool IsEmpty() => errors.Count == 0;
        public List<Error> GetAll() => errors;


    }
}
