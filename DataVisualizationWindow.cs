using System.Windows;
using System.Windows.Controls;

namespace DataVisualization
{
    public class DataVisualizationWindow : Window
    {
        public DataVisualizationBaseControl BaseControl { get { return Content as DataVisualizationBaseControl; } }

        public DataVisualizationControl Visualizer { get { return BaseControl?.Control; } }

        public WrapPanel Panel { get { return BaseControl?.Panel; } }

        public UIElementCollection Interactors { get { return Panel?.Children; } }

        public DataVisualizationWindow()
        {
            Content = new DataVisualizationBaseControl();
        }
    }
}
