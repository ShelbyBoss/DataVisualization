using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DataVisualization
{
    public class DataContainerCollection : ObservableCollection<DataContainer>
    {
        private DataVisualizationControl parent;

        internal DataContainerCollection(DataVisualizationControl parent)
        {
            this.parent = parent;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var newContainers = e.NewItems?.OfType<DataContainer>() ?? Enumerable.Empty<DataContainer>();
            foreach (DataContainer container in newContainers)
            {
                container.parents.Add(parent);
            }

            var oldContainers = e.NewItems?.OfType<DataContainer>() ?? Enumerable.Empty<DataContainer>();
            foreach (DataContainer container in oldContainers)
            {
                container.parents.Remove(parent);
            }

            base.OnCollectionChanged(e);

            parent.InvalidateVisual();
        }
    }
}
