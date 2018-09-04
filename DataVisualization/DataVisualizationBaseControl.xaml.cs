using System.Windows.Controls;

namespace DataVisualization
{
    /// <summary>
    /// Interaktionslogik für DataVisualizer.xaml
    /// </summary>
    public partial class DataVisualizationBaseControl : UserControl
    {
        public DataVisualizationControl Control { get { return visualizerControl; } }
        public WrapPanel Panel { get { return panel; } }

        public DataVisualizationBaseControl()
        {
            InitializeComponent();
        }
    }
}
