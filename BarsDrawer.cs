using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    class BarsDrawer : Drawer
    {
        public BarsDrawer(DataContainer container) : base(container)
        {
        }

        protected override void Draw(DrawingContext drawingContext)
        {
            Pen pen = new Pen();

            foreach (Point value in container.Array)
            {
                Point topMiddle = ToPixel(value);
                double x = topMiddle.X - container.Thickness / 2.0;
                double y = topMiddle.Y;
                double w = container.Thickness;
                double h = visualizer.ActualHeight - topMiddle.Y;
                Rect rect = new Rect(x, y, w, h);

                drawingContext.DrawRectangle(container.Brush, pen, rect);
            }
        }
    }
}
