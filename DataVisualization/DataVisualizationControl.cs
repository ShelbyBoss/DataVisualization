using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DataVisualization
{
    public enum VisualizitionMode { Points, Line, Bars }

    public class DataVisualizationControl : UserControl
    {
        private TextBlock tblMouseValue;

        internal Rect DataRect { get; private set; }

        public DataContainerCollection Source { get; private set; }

        public DataVisualizationControl()
        {
            Source = new DataContainerCollection(this);

            var dpd = DependencyPropertyDescriptor.FromProperty(ForegroundProperty, typeof(Control));
            dpd.AddValueChanged(this, OnForegroundPropertyChanged);

            MouseMove += DataVisualizer_MouseMove;
            MouseLeave += DataVisualizer_MouseLeave;

            Content = tblMouseValue = new TextBlock()
            {
                Visibility = Visibility.Collapsed,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
        }

        private void OnForegroundPropertyChanged(object sender, EventArgs e)
        {
            tblMouseValue.Foreground = Foreground;
        }

        private void DataVisualizer_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = Mouse.GetPosition(this);

            if (mousePos.X < 0 || mousePos.X > ActualWidth || mousePos.Y < 0 || mousePos.Y > ActualHeight)
            {
                tblMouseValue.Visibility = Visibility.Collapsed;
            }
            else
            {
                double mouseValueX = mousePos.X / ActualWidth * DataRect.Width + DataRect.X;
                double mouseValueY = (1 - mousePos.Y / ActualHeight) * DataRect.Height + DataRect.Y;

                tblMouseValue.Text = Round(mouseValueX) + "\n" + Round(mouseValueY);
                tblMouseValue.Visibility = Visibility.Visible;

                bool overlapX = mousePos.X + tblMouseValue.ActualWidth > ActualWidth;
                bool overlapY = mousePos.Y + tblMouseValue.ActualHeight > ActualHeight;
                double x = !overlapX ? mousePos.X : mousePos.X - tblMouseValue.ActualWidth;
                double y = !overlapY ? mousePos.Y : mousePos.Y - tblMouseValue.ActualHeight;

                tblMouseValue.TextAlignment = !overlapX ? TextAlignment.Left : TextAlignment.Right;

                tblMouseValue.Margin = new Thickness(x, y, 0, 0);
            }
        }

        private void DataVisualizer_MouseLeave(object sender, MouseEventArgs e)
        {
            tblMouseValue.Visibility = Visibility.Collapsed;
        }

        private string Round(double value)
        {
            int round = -Convert.ToInt32(Math.Log10(DataRect.Width * 0.01)) + 1;
            return Math.Round(value, round).ToString();
        }

        private void SetDataRect()
        {
            double minX = Source.SelectMany(c=>c.Array).Min(p => p.X);
            double minY = Source.SelectMany(c => c.Array).Min(p => p.Y);
            double rangeX = Source.SelectMany(c => c.Array).Max(p => p.X) - minX;
            double rangeY = Source.SelectMany(c => c.Array).Max(p => p.Y) - minY;

            DataRect = new Rect(minX, minY, rangeX, rangeY);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Source.Count == 0) return;

            SetDataRect();

            double width = ActualWidth;
            double height = ActualHeight;
            Size size = new Size(width, height);

            foreach (DataContainer container in Source) container.Drawer.Draw(drawingContext, this);

            CultureInfo ci = CultureInfo.CurrentCulture;
            FlowDirection fd = FlowDirection.LeftToRight;
            Typeface tf = new Typeface("Arial");
            double es = FontSize;
            Brush foreground = Foreground;

            FormattedText ftMinX = new FormattedText(DataRect.X.ToString(), ci, fd, tf, es, foreground);
            FormattedText ftMaxX = new FormattedText(DataRect.Right.ToString(), ci, fd, tf, es, foreground);
            FormattedText ftMinY = new FormattedText(DataRect.Y.ToString(), ci, fd, tf, es, foreground);
            FormattedText ftMaxY = new FormattedText(DataRect.Bottom.ToString(), ci, fd, tf, es, foreground);

            drawingContext.DrawText(ftMaxY, new Point(0, 0));
            drawingContext.DrawText(ftMinY, new Point(0, height - 2 * es));
            drawingContext.DrawText(ftMinX, new Point(ftMinY.Width, height - es));
            drawingContext.DrawText(ftMaxX, new Point(width - ftMaxX.Width, height - es));
        }
    }
}
