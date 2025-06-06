using pixel_walle.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using pixel_walle.src;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using pixel_walle.src.Canvas;

namespace pixel_walle.controllers
{
    public class CanvasRender
    {
        private readonly Canvas canvasControl;
        private readonly Context context;
        private readonly int pixelSize;

        public CanvasRender(Canvas canvasControl, Context context, int pixelSize = 10)
        {
            this.canvasControl = canvasControl;
            this.context = context;
            this.pixelSize = pixelSize;
        }

        public void Render()
        {
            canvasControl.Children.Clear();

            for (int x = 0; x < context.canvas.Width; x++)
            {
                for (int y = 0; y < context.canvas.Height; y++)
                {
                    Pixel pixel = context.canvas.pixels[x, y];

                    var rect = new Rectangle
                    {
                        Width = pixelSize,
                        Height = pixelSize,
                        Fill = new SolidColorBrush(pixel.Color_)
                    };

                    Canvas.SetLeft(rect, x * pixelSize);
                    Canvas.SetTop(rect, y * pixelSize);

                    canvasControl.Children.Add(rect);
                }
            }


            if (context.HasRobot)
            {
                var robot = context.Robot;
                var brushColor = context.Brush.ColorBrush.ToString();
                var iconSource = GetIcon(brushColor);

                var robotImage = new Image
                {
                    Width = pixelSize,
                    Height = pixelSize,
                    Source = iconSource
                };

                Canvas.SetLeft(robotImage, robot.X * pixelSize);
                Canvas.SetTop(robotImage, robot.Y * pixelSize);

                canvasControl.Children.Add(robotImage);

            }

        }



    public static readonly Dictionary<string, string> IconPaths = new()
    {
        ["Red"] = "pack://application:,,,/assets/red.png",
        ["Blue"] = "pack://application:,,,/assets/blue.png",
        ["Green"] = "pack://application:,,,/assets/green.png",
        ["Yellow"] = "pack://application:,,,/assets/yellow.png",
        ["Orange"] = "pack://application:,,,/assets/orange.png",
        ["Purple"] = "pack://application:,,,/assets/purple.png",
        ["Black"] = "pack://application:,,,/assets/black.png",
        ["White"] = "pack://application:,,,/assets/transparent.png"
    };

        public static ImageSource GetIcon(string color)
        {
            if (IconPaths.TryGetValue(color, out var path))
            { 
                return new BitmapImage(new Uri(path));
            }
            return new BitmapImage(new Uri("pack://application:,,,/Assets/default.png"));
        }
    }

}
