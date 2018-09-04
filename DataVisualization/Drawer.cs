using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    abstract class Drawer
    {
        protected DataContainer container;
        protected DataVisualizationControl visualizer;

        public Drawer(DataContainer container)
        {
            this.container = container;
        }

        public void Draw(DrawingContext drawingContext, DataVisualizationControl visualizer)
        {
            this.visualizer = visualizer;

            Draw(drawingContext);
        }

        protected abstract void Draw(DrawingContext drawingContext);

        protected Point ToPixel(Point value)
        {
            double x = visualizer.ActualWidth * (value.X - visualizer.DataRect.X) / visualizer.DataRect.Width;
            double y = visualizer.ActualHeight * (1 - (value.Y - visualizer.DataRect.Y) / visualizer.DataRect.Height);
            return new Point(x, y);
        }
    }
}
