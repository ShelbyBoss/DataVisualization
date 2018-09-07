using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    class LineDrawer : Drawer
    {
        public LineDrawer(DataContainer container) : base(container)
        {
        }

        protected override void Draw(DrawingContext drawingContext)
        {
            Pen pen = new Pen(container.Brush, container.Thickness);
            Point prePosition = ToPixel(container.Array[0]);

            foreach (Point value in container.Array.Skip(1))
            {
                Point curPosition = ToPixel(value);
                drawingContext.DrawLine(pen, prePosition, curPosition);
                prePosition = curPosition;
            }
        }
    }
}
