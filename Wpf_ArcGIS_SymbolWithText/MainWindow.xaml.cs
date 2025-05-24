using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using Esri.ArcGISRuntime;

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
                MakePointGraphicsOverlay2(),
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

                // ひし形シンボルを作成
                SimpleMarkerSymbol pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Diamond, Color.Green, 20);

                // テキストシンボルを作成
                TextSymbol textSymbol = new TextSymbol
                {
                    Text = "右上添字Meiryo14",
                    Color = System.Drawing.Color.Blue,
                    Size = 14,
                    FontFamily = "Meiryo",
                    OffsetX = -10,
                    OffsetY = -10,
                    HorizontalAlignment = Esri.ArcGISRuntime.Symbology.HorizontalAlignment.Right,
                    VerticalAlignment = Esri.ArcGISRuntime.Symbology.VerticalAlignment.Top
                };

                // 表示オーバレイ
                GraphicsOverlay pointGraphicsOverlay = new GraphicsOverlay();

                // 要らないの？
                // pointGraphicsOverlay.Renderer = new SimpleRenderer(pointSymbol);

                // ポイントの作成
                var pointGeometry = new MapPoint(138.727363, 35.360626, SpatialReferences.Wgs84);

                // グラフィックにひし形シンボルを追加
                var pointGraphic = new Graphic(pointGeometry, pointSymbol);
                pointGraphicsOverlay.Graphics.Add(pointGraphic);

                // グラフィックにひし形シンボルを追加
                var textGraphic = new Graphic(pointGeometry, textSymbol);
                pointGraphicsOverlay.Graphics.Add(textGraphic);

                return pointGraphicsOverlay;
            }
            catch (ArcGISRuntimeException ex)
            {
                // ArcGIS Runtime固有のエラーを処理
                Console.WriteLine($"ArcGIS Runtimeエラー検出: {ex.Message}");
                throw;
            }
        }


        private GraphicsOverlay MakePointGraphicsOverlay2()
        {
            // ひし形シンボルを作成
            SimpleMarkerSymbol pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, Color.Green, 20);

            // テキストシンボルを作成
            TextSymbol textSymbol = new TextSymbol
            {
                Text = "左下添字Msゴシック9",
                Color = System.Drawing.Color.Red,
                Size = 9,
                OffsetX = 10,
                OffsetY = 10,
                FontFamily = "MS Gothic",
                HorizontalAlignment = Esri.ArcGISRuntime.Symbology.HorizontalAlignment.Left,
                VerticalAlignment = Esri.ArcGISRuntime.Symbology.VerticalAlignment.Bottom
            };

            // 表示オーバレイ
            GraphicsOverlay pointGraphicsOverlay = new GraphicsOverlay();

            // 要らないの？
            // pointGraphicsOverlay.Renderer = new SimpleRenderer(pointSymbol);

            // ポイントの作成
            var pointGeometry = new MapPoint(138.677363, 35.310626, SpatialReferences.Wgs84);

            // グラフィックにひし形シンボルを追加
            var pointGraphic = new Graphic(pointGeometry, pointSymbol);
            pointGraphicsOverlay.Graphics.Add(pointGraphic);

            // グラフィックにひし形シンボルを追加
            var textGraphic = new Graphic(pointGeometry, textSymbol);
            pointGraphicsOverlay.Graphics.Add(textGraphic);

            return pointGraphicsOverlay;
        }
    }
}
