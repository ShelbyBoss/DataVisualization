using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DataVisualization
{
    public static class VisualFactory
    {
        public static bool? Do(IEnumerable<Point> data, VisualizitionMode mode = VisualizitionMode.Line,
            double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, mode, thickness)), AsEnumerable<UIElement>());
        }

        public static bool? Do(IEnumerable<Point> data, Brush brush,
            VisualizitionMode mode = VisualizitionMode.Line, double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, brush, mode, thickness)), AsEnumerable<UIElement>());
        }

        public static bool? Do(DataContainer container)
        {
            return Do(AsEnumerable(container), AsEnumerable<UIElement>());
        }

        public static bool? Do(IEnumerable<DataContainer> containers)
        {
            return Do(containers, AsEnumerable<UIElement>());
        }


        public static bool? Do(IEnumerable<Point> data, UIElement interactor, VisualizitionMode mode = VisualizitionMode.Line,
            double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, mode, thickness)), AsEnumerable(interactor));
        }

        public static bool? Do(IEnumerable<Point> data, Brush brush, UIElement interactor,
            VisualizitionMode mode = VisualizitionMode.Line, double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, brush, mode, thickness)), AsEnumerable(interactor));
        }

        public static bool? Do(DataContainer container, UIElement interactor)
        {
            return Do(AsEnumerable(container), AsEnumerable(interactor));
        }

        public static bool? Do(IEnumerable<DataContainer> containers, UIElement interactor)
        {
            return Do(containers, AsEnumerable(interactor));
        }


        public static bool? Do(IEnumerable<Point> data, IEnumerable<UIElement> interactors,
            VisualizitionMode mode = VisualizitionMode.Line, double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, mode, thickness)), interactors);
        }

        public static bool? Do(IEnumerable<Point> data, Brush brush, IEnumerable<UIElement> interactors,
            VisualizitionMode mode = VisualizitionMode.Line, double thickness = DataContainer.DefaultThickness)
        {
            return Do(AsEnumerable(DataContainer.Get(data, brush, mode, thickness)), interactors);
        }

        public static bool? Do(DataContainer container, IEnumerable<UIElement> interactors)
        {
            return Do(AsEnumerable(container), interactors);
        }

        public static bool? Do(IEnumerable<DataContainer> containers, IEnumerable<UIElement> interactors)
        {
            DataVisualizationWindow window = new DataVisualizationWindow();

            foreach (DataContainer container in containers) window.Visualizer.Source.Add(container);
            foreach (UIElement interactor in interactors) window.Interactors.Add(interactor);

            return window.ShowDialog();
        }


        public static IEnumerable<T> AsEnumerable<T>(params T[] objs)
        {
            foreach (T obj in objs) yield return obj;
        }
    }
}
