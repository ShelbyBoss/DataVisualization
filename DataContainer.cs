using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    public class DataContainer
    {
        internal List<DataVisualizationControl> parents = new List<DataVisualizationControl>();

        private IEnumerable<Point> data;
        private Brush brush;
        private double thickness;
        private VisualizitionMode mode;

        public IEnumerable<Point> Data
        {
            get { return data; }
            set
            {
                if (value == data) return;

                bool equal = data != null && value != null && data.SequenceEqual(value);

                if (data is INotifyCollectionChanged oldSource) oldSource.CollectionChanged -= OnCollectionChanged;
                if (value is INotifyCollectionChanged newSource) newSource.CollectionChanged += OnCollectionChanged;

                data = value;
                Array = data?.ToArray();

                if (!equal) NotifyParents();
            }
        }

        internal Point[] Array { get; private set; }

        public Brush Brush
        {
            get { return brush; }
            set
            {
                if (value == brush) return;

                brush = value;
                NotifyParents();
            }
        }

        public double Thickness
        {
            get { return thickness; }
            set
            {
                if (value == thickness) return;

                thickness = value;
                NotifyParents();
            }
        }

        public VisualizitionMode Mode
        {
            get { return mode; }
            set
            {
                if (value == mode) return;

                mode = value;

                switch (mode)
                {
                    case VisualizitionMode.Points:
                        Drawer = new PointsDrawer(this);
                        break;
                    case VisualizitionMode.Line:
                        Drawer = new LineDrawer(this);
                        break;
                    case VisualizitionMode.Bars:
                        Drawer = new BarsDrawer(this);
                        break;
                }

                NotifyParents();
            }
        }

        internal Drawer Drawer { get; private set; }

        public DataContainer()
        {
            Mode = VisualizitionMode.Line;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyParents();
        }

        private void NotifyParents()
        {
            foreach (DataVisualizationControl parent in parents) parent.InvalidateVisual();
        }

        public const double DefaultThickness = 5;
        public static readonly Brush DefaultBrush = Brushes.Blue;

        public static DataContainer Get(IEnumerable<Point> data, VisualizitionMode mode = VisualizitionMode.Line,
            double thickness = DataContainer.DefaultThickness)
        {
            return Get(data, DefaultBrush, mode, thickness);
        }

        public static DataContainer Get(IEnumerable<Point> data, Brush brush,
            VisualizitionMode mode = VisualizitionMode.Line, double thickness = DefaultThickness)
        {
            return new DataContainer()
            {
                Data = data,
                Brush = brush,
                Mode = mode,
                Thickness = thickness
            };
        }
    }
}
