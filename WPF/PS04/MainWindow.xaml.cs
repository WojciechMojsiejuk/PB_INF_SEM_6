using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Zadanie4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle lastRectangle;
        private Point startPoint;
        private Rectangle rectangle;
        private double x;
        private double y;
        public MainWindow()
        {
            lastRectangle = new Rectangle();
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MoveRectangle);
            timer.Tick += new EventHandler(ChangeSize);
            timer.Start();
        }

        private void Canvas_startDrawing(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(mainCanvas);
            rectangle = new Rectangle
            {
                Stroke=Brushes.Black,
                StrokeThickness = 1
            };
            Canvas.SetLeft(rectangle, startPoint.X);
            Canvas.SetTop(rectangle, startPoint.Y);
            mainCanvas.Children.Add(rectangle);
        }

        private void Canvas_endDrawing(object sender, MouseButtonEventArgs e)
        {
            lastRectangle = rectangle;
        }

        private void Canvas_draw(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rectangle == null)
                return;
            var position = e.GetPosition(mainCanvas);
            var x = Math.Min(position.X, startPoint.X);
            var y = Math.Min(position.Y, startPoint.Y);
            var w = Math.Max(position.X,startPoint.X)-x;
            var h = Math.Max(position.Y, startPoint.Y) - y;
            rectangle.Width = w;
            rectangle.Height = h;
            this.x = x;
            this.y = y;
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);

        }

        private void MoveRectangle(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
                return;
            else if (Keyboard.IsKeyDown(Key.Left))
            {
                x -= .05;
                Canvas.SetLeft(lastRectangle, x);
            }
            else if (Keyboard.IsKeyDown(Key.Up))
            {
                y -= .05;
                Canvas.SetTop(lastRectangle, y);
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                y += .05;
                Canvas.SetTop(lastRectangle, y);
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                x += .05;
                Canvas.SetLeft(lastRectangle, x);
            }
        }

        private void ChangeSize(object sender, EventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftShift))
                return;
            else if (Keyboard.IsKeyDown(Key.Left) && lastRectangle.Width>10 )
            {
                lastRectangle.Width -= 0.1;
                Canvas.SetLeft(lastRectangle, x);
            }
            else if (Keyboard.IsKeyDown(Key.Up) && lastRectangle.Height> 10 )
            {
                lastRectangle.Height -= 0.1;
                Canvas.SetTop(lastRectangle, y);
            }
            else if (Keyboard.IsKeyDown(Key.Down) && Canvas.GetTop(lastRectangle)+lastRectangle.Height<(mainWindow.Height-40))
            {
                lastRectangle.Height += 0.1;
                Canvas.SetTop(lastRectangle, y);
            }
            else if (Keyboard.IsKeyDown(Key.Right) && Canvas.GetLeft(lastRectangle) + lastRectangle.Width < (mainWindow.Width - 20))
            {
                lastRectangle.Width += 0.1;
                Canvas.SetLeft(lastRectangle, x);
            }

        }
    }
}
