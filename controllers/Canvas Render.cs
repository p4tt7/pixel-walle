using pixel_walle.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using pixel_walle.src.Canvas;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.IO;

namespace pixel_walle.controllers
{
    public class CanvasRender
    {
        private readonly UniformGrid grid;
        private readonly Context context;

        public CanvasRender(UniformGrid grid, Context context)
        {
            this.grid = grid;
            this.context = context;
        }

        public void Render()
        {
            grid.Rows = context.canvas.Height;
            grid.Columns = context.canvas.Width;

            if (grid.Children.Count == 0)
            {
                InitializeGrid();
            }

            UpdateModifiedPixels();

            RenderRobot();
        }


        private void InitializeGrid()
        {
            grid.Children.Clear();
            for (int y = 0; y < context.canvas.Height; y++)
            {
                for (int x = 0; x < context.canvas.Width; x++)
                {
                    var rect = new Border
                    {
                        Width = grid.Width / grid.Columns,
                        Height = grid.Height / grid.Rows,
                        Background = new SolidColorBrush(context.canvas.pixels[x, y].Color),
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(0.2)
                    };
                    grid.Children.Add(rect);
                }
            }
        }

        private void UpdateModifiedPixels()
        {
            for (int y = 0; y < context.canvas.Height; y++)
            {
                for (int x = 0; x < context.canvas.Width; x++)
                {
                    if (!context.canvas.changes[x, y]) continue;

                    var index = y * context.canvas.Width + x;
                    if (index >= grid.Children.Count) continue;

                    var rect = grid.Children[index] as Border;
                    if (rect != null)
                    {
                        rect.Background = new SolidColorBrush(context.canvas.pixels[x, y].Color);
                        context.canvas.changes[x, y] = false;
                    }
                }
            }
        }

        private void RenderRobot()
        {
            if (!context.HasRobot) return;

            ClearPreviousRobotPosition();

            var robotIndex = context.Robot.Y * context.canvas.Width + context.Robot.X;
            if (robotIndex < grid.Children.Count)
            {
                var rect = grid.Children[robotIndex] as Border;
                if (rect != null)
                {
                    var brushColor = context.Brush.ColorBrush.ToString();
                    var iconSource = GetIcon(brushColor);
                    rect.Child = new Image
                    {
                        Source = iconSource,
                        Stretch = Stretch.Fill
                    };
                }
            }
        }

        private void ClearPreviousRobotPosition()
        {
            foreach (var child in grid.Children)
            {
                if (child is Border border)
                {
                    border.Child = null;
                }
            }
        }

        public static readonly Dictionary<string, string> IconPaths = new()
        {
            ["FFFF0000"] = "assets/red.png",        
            ["FF0000FF"] = "assets/blue.png",
            ["FF00FF00"] = "assets/green.png",
            ["FFFFFF00"] = "assets/yellow.png",
            ["FFFFA500"] = "assets/orange.png",  
            ["FF800080"] = "assets/purple.png",
            ["FF000000"] = "assets/black.png",
            ["FFFFFFFF"] = "assets/transparent.png" 
        };

        public static ImageSource GetIcon(string color)
        {
            var hex = color.Replace("#", "").ToUpperInvariant();

            try
            {
                var uri = new Uri($"pack://application:,,,/pixel_walle;component/assets/{hex}.png");
                return new BitmapImage(uri);
            }
            catch
            {
                return new BitmapImage(new Uri("pack://application:,,,/pixel_walle;component/assets/transparent.png"));
            }
        }



    }

}
