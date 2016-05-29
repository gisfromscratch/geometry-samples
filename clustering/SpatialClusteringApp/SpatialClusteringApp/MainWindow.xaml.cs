using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using Spatial.Clustering.Algorithms;
using Spatial.Clustering.Data;
using SpatialClusteringApp.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SpatialClusteringApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
            {
                return;
            }

            Debug.WriteLine(string.Format("Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message));
        }

        private void MapView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nameof(MapView.SpatialReference).Equals(e.PropertyName) && null != MapView.SpatialReference)
            {
                Task.Run(() =>
                {
                    // Load the data
                    var inputFile = new FileInfo(Settings.Default.GeonamesFilePath);
                    var delimiter = Settings.Default.Delimiter;
                    switch (delimiter)
                    {
                        case @"\t":
                            delimiter = "\t";
                            break;
                    }
                    var clusterFile = new ClusterPointTextFile(inputFile, delimiter, Settings.Default.XTokenIndex, Settings.Default.YTokenIndex, Settings.Default.StartRowIndex);
                    var distanceCalculator = new EuclideanDistanceCalculator();
                    var clusterer = new KMeansClusterer(distanceCalculator);
                    return clusterer.CreateClusters(clusterFile, Settings.Default.ClusterCount);
                }).ContinueWith((clusterTask)=> {
                    // Create the graphics
                    var clusters = clusterTask.Result;
                    var random = new Random();
                    var wgs84 = SpatialReference.Create(4326);
                    foreach (var cluster in clusters)
                    {
                        // Create a graphic layer
                        var layer = new GraphicsLayer();
                        layer.RenderingMode = GraphicsRenderingMode.Static;
                        foreach (var element in cluster.Elements)
                        {
                            var clusterPoint = element as ClusterPoint;
                            if (null != clusterPoint)
                            {
                                var mapPoint = new MapPoint(clusterPoint.X, clusterPoint.Y, wgs84);
                                var projectedMapPoint = GeometryEngine.Project(mapPoint, MapView.SpatialReference);
                                var graphic = new Graphic(projectedMapPoint);
                                layer.Graphics.Add(graphic);
                            }
                        }

                        // Create a marker symbol
                        var markerSymbol = new SimpleMarkerSymbol();
                        markerSymbol.Style = SimpleMarkerStyle.Circle;
                        markerSymbol.Size = 6;
                        markerSymbol.Color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

                        // Create a renderer
                        var renderer = new SimpleRenderer();
                        renderer.Symbol = markerSymbol;
                        layer.Renderer = renderer;

                        MapView.Map.Layers.Add(layer);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
