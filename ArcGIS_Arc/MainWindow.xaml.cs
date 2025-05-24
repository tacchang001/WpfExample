using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;

/*
 * https://github.com/Esri/arcgis-maps-sdk-dotnet-samples/blob/main/src/WPF/WPF.Viewer/Samples/GraphicsOverlay/AddGraphicsRenderer/AddGraphicsRenderer.xaml.cs
 *
 * https://github.com/Esri/arcgis-maps-sdk-dotnet-samples/tree/main/src/WPF
 * https://github.com/Esri/arcgis-maps-sdk-dotnet-samples/blob/main/src/WPF/WPF.Viewer/Samples/GraphicsOverlay/
 */
namespace Wpf_ArcGIS_SymbolWithText
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var license = Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.GetLicense();
            Console.WriteLine($"ESRI Lincense : {license}");

            MapPoint mapCenterPoint = new MapPoint(138.727363, 35.360626, SpatialReferences.Wgs84);
            MainMapView.SetViewpoint(new Viewpoint(mapCenterPoint, 200000.0));

            // Add graphics overlays to the map view.
            MainMapView.GraphicsOverlays.AddRange(new[]
            {
                MakePointGraphicsOverlay1(),
            });
        }

        internal static async Task CheckApiKey()
        {
            try
            {
                // テスト用にベースマップをロード
                var map = new Map(BasemapStyle.ArcGISLightGray);

                // 有効なAPI Keyを確認するため、ベースマップロードを試みる
                await map.LoadAsync();

                // 正常にロードできた場合、API Keyは有効
                if (map.LoadStatus == LoadStatus.Loaded)
                {
                    Console.WriteLine("API Keyは有効です。");
                }
                else
                {
                    Console.WriteLine("API Keyが無効です。");
                }
            }
            catch (Exception ex)
            {
                // エラー処理：無効なAPI Keyまたはその他の問題
                Console.WriteLine($"エラーが発生しました: {ex.Message}");
            }
        }

        private GraphicsOverlay MakePointGraphicsOverlay1()
        {
            try
            {
                // API Keyの正当性確認
                Task task = CheckApiKey();

                // 円弧の中心
                MapPoint center = new MapPoint(138.727363, 35.360626, SpatialReferences.Wgs84); // 東京の座標

                // 円弧の計算
                List<MapPoint> points = new List<MapPoint>();
                double radius = 0.1; // 距離の半径（度単位）
                for (double angle = 0; angle <= 180; angle += 10) // 0度から180度まで
                {
                    double radian = Math.PI * angle / 180.0;
                    double x = center.X + radius * Math.Cos(radian);
                    double y = center.Y + radius * Math.Sin(radian);
                    points.Add(new MapPoint(x, y, SpatialReferences.Wgs84));
                }

                // ポリライン作成
                Polyline arcPolyline = new Polyline(points);

                GraphicsOverlay pointGraphicsOverlay = new GraphicsOverlay();

                // グラフィック追加
                var pointGraphic = new Graphic(arcPolyline, new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Red, 2));
                pointGraphicsOverlay.Graphics.Add(pointGraphic);

                return pointGraphicsOverlay;
            }
            catch (ArcGISRuntimeException ex)
            {
                // ArcGIS Runtime固有のエラーを処理
                Console.WriteLine($"ArcGIS Runtimeエラー検出: {ex.Message}");
                throw;
            }
        }
    }
}
