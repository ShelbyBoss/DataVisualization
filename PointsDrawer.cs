using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    class PointsDrawer : Drawer
    {
        public PointsDrawer(DataContainer container) : base(container)
        {
        }

        protected override void Draw(DrawingContext drawingContext)
        {
            Pen pen = new Pen(container.Brush, 0);

            foreach (Point value in container.Array)
            {
                Point center = ToPixel(value);

                drawingContext.DrawEllipse(container.Brush, pen, center, container.Thickness, container.Thickness);
            }
        }
    }
}
