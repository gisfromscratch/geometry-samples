using Esri.ArcGISRuntime.Controls;
using System.Diagnostics;
using System.Windows;

namespace SpatialClusteringApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyMapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }
    }
}
