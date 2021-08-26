using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;

using Signal_Viewer.Type.Signal;
using Signal_Viewer.Simulation.Signal;

namespace Signal_Viewer {
    public partial class MainWindow : Window {
        private const double settingAreaWidth = 250;
        private const double margin = 10;
        private const double sizeTitleWindow = 31;
        private const double sizeBorderWindow = 8;

        private double minGridWidth = 2 * settingAreaWidth;

        private double signalLenght = 0;
        private double signalMultiplier = 20;
        private double totalDisplayFactorInPixels = 2;

        private List<Point> pointsOnGraph = new List<Point>();

        double centerOfSignalAreaCanvasInHeight;
        double centerOfRadialChartCanvasInWidth;
        double centerOfRadialChartCanvasInHeight;

        private AggregateSignal aggregateSignal;
        private AggregateSignalSimulation aggregateSignalSimulation;

        public MainWindow() {
            InitializeComponent();
            CanvasSetting();
            AggregateSignalSimulationSetting();
            
            TimerSetting();

            this.MinWidth = this.Width;
            this.MinHeight = this.Height;
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            GridSetting(sender);
        }
        private void GridSetting(object sender) {
            Window mainWindow;
            
            if (sender is Window window) {
                mainWindow = window;
            } else {
                return;
            }

            double gridActualHeight = mainWindow.ActualHeight - (sizeTitleWindow + sizeBorderWindow);
            double gridActualWidth = mainWindow.ActualWidth - (2 * sizeBorderWindow);

            if (gridActualWidth >= minGridWidth) {
                SettingArea.Width = new GridLength(settingAreaWidth);

                double radialChartWidth = gridActualHeight - (2 * margin);
                double signalAreaWidth = GetSignalAreaWidth(radialChartWidth);

                if (signalAreaWidth >= radialChartWidth) {
                    RadialChart.Width = new GridLength(radialChartWidth);
                } else {
                    signalAreaWidth += radialChartWidth;
                    RadialChart.Width = new GridLength(0);
                }
                SignalArea.Width = new GridLength(signalAreaWidth);
            }

            double GetSignalAreaWidth(double radialChartWidth) {
                return gridActualWidth - (radialChartWidth + SettingArea.Width.Value);
            }
        }

        private void CanvasSetting() {
            SignalAreaCanvas.Margin = new Thickness(margin, margin, 0, margin);
            RadialChartCanvas.Margin = new Thickness(0, margin, 0, margin);
            SettingAreaCanvas.Margin = new Thickness(margin, margin, margin, margin);

            centerOfSignalAreaCanvasInHeight = SignalAreaCanvas.ActualHeight / 2;
            centerOfRadialChartCanvasInWidth = RadialChartCanvas.ActualWidth / 2;
            centerOfRadialChartCanvasInHeight = RadialChartCanvas.ActualHeight / 2;
        }
        private void SettingShow() {
            aggregateSignalSimulation.RegisterSimulationResultHandler(ViewSimulationResult);
        }
        private void TimerSetting() {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTask);
            timer.Interval = new TimeSpan(days: 0, 
                                          hours: 0, 
                                          minutes: 0, 
                                          seconds: 0, 
                                          milliseconds: 100);
            timer.Start();
        }
        private void TimerTask(object obj, EventArgs e) {
            aggregateSignalSimulation?.SimulationStep();
        }

        private void AggregateSignalSimulationSetting() {
            AggregateSignalSetting();

            aggregateSignalSimulation = new AggregateSignalSimulation(aggregateSignal);

            SettingShow();
        }
        private void AggregateSignalSetting() {
            aggregateSignal = new AggregateSignal();

            //aggregateSignal.AddSignalComponent(new HarmSignal(1, 2, GetDegreesIntoRadians(0)));

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 2, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(2, 3, GetDegreesIntoRadians(45)));
            aggregateSignal.AddSignalComponent(new HarmSignal(3, 4, GetDegreesIntoRadians(180)));
            aggregateSignal.AddSignalComponent(new HarmSignal(4, 1, GetDegreesIntoRadians(135)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 2, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 4, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 6, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 8, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 10, GetDegreesIntoRadians(0)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 2, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 4, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 6, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 8, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 10, GetDegreesIntoRadians(0)));
            //*/

            //*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 2, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 4, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 6, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 8, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 10, GetDegreesIntoRadians(0)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 2, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 4, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 6, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 8, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 10, GetDegreesIntoRadians(0)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 2, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 4, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 6, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 8, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 10, GetDegreesIntoRadians(90)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 2, GetDegreesIntoRadians(80)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 3, GetDegreesIntoRadians(20)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 4, GetDegreesIntoRadians(60)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 5, GetDegreesIntoRadians(40)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.5, 6, GetDegreesIntoRadians(50)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 7, GetDegreesIntoRadians(30)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.3, 8, GetDegreesIntoRadians(70)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2, 9, GetDegreesIntoRadians(10)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.1, 10, GetDegreesIntoRadians(90)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1, 1, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.4, 2, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.35, 3, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.32, 4, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.30, 5, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.29, 6, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.285, 7, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.2825, 8, GetDegreesIntoRadians(90)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.28245, 9, GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.282448, 10, GetDegreesIntoRadians(90)));
            //*/

            /*
            aggregateSignal.AddSignalComponent(new HarmSignal(1,   1,  GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 2,  GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 3,  GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 4,  GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 5,  GetDegreesIntoRadians(0)));
            aggregateSignal.AddSignalComponent(new HarmSignal(1,   6,  GetDegreesIntoRadians(180)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.9, 7,  GetDegreesIntoRadians(180)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.8, 8,  GetDegreesIntoRadians(180)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.7, 9,  GetDegreesIntoRadians(180)));
            aggregateSignal.AddSignalComponent(new HarmSignal(0.6, 10, GetDegreesIntoRadians(180)));
            //*/

            double GetDegreesIntoRadians(double degrees) => (Math.PI * degrees) / 180;
        }

        private void ViewSimulationResult(double signalAmplitude, List<(double amplitude, double phase)> signalComponentsParameters) {
            ViewSimulationAggregateSignal(signalAmplitude);
            ViewSimulationRadialChart(signalComponentsParameters);
        }
        private void ViewSimulationAggregateSignal(double signalAmplitude) {

            SolidColorBrush ColorBrush;
            Polyline GraphPolyline;
            PointCollection polygonPoints;

            if (centerOfSignalAreaCanvasInHeight != SignalAreaCanvas.ActualHeight / 2) {
                double actualCenter = SignalAreaCanvas.ActualHeight / 2;

                for (int i = 0; i < pointsOnGraph.Count; i++) {
                    pointsOnGraph[i] = new Point(pointsOnGraph[i].X, pointsOnGraph[i].Y + (actualCenter >= centerOfSignalAreaCanvasInHeight ? actualCenter - centerOfSignalAreaCanvasInHeight : (centerOfSignalAreaCanvasInHeight - actualCenter) * -1));
                }

                centerOfSignalAreaCanvasInHeight = SignalAreaCanvas.ActualHeight / 2;
            }

            SignalSetting(Colors.Black, strokeThickness: 1);

            if (signalLenght < SignalAreaCanvas.ActualWidth) {
                pointsOnGraph.Add(new Point(signalLenght, (signalMultiplier * signalAmplitude) + centerOfSignalAreaCanvasInHeight));
                
                signalLenght += totalDisplayFactorInPixels;
            } else {
                if (signalLenght / totalDisplayFactorInPixels > SignalAreaCanvas.ActualWidth / totalDisplayFactorInPixels) {
                    while (pointsOnGraph.Count > SignalAreaCanvas.ActualWidth / totalDisplayFactorInPixels) {
                        GraphPolyline.Points.Remove(pointsOnGraph[0]);
                        pointsOnGraph.Remove(pointsOnGraph[0]);
                    }

                    for (int i = 0; i < pointsOnGraph.Count; i++) {
                        pointsOnGraph[i] = new Point(pointsOnGraph[i].X - (signalLenght - SignalAreaCanvas.ActualWidth), pointsOnGraph[i].Y);
                    }

                    signalLenght = SignalAreaCanvas.ActualWidth;
                }

                GraphPolyline.Points.Remove(pointsOnGraph[0]);
                pointsOnGraph.Remove(pointsOnGraph[0]);

                pointsOnGraph.Add(new Point(signalLenght, (signalMultiplier * signalAmplitude) + centerOfSignalAreaCanvasInHeight));

                for(int i = 0; i < pointsOnGraph.Count; i++) {
                    pointsOnGraph[i] = new Point(pointsOnGraph[i].X - totalDisplayFactorInPixels, pointsOnGraph[i].Y);
                }
            }

            foreach (var item in pointsOnGraph) {
                polygonPoints.Add(item);
            }

            polygonPoints.Add(new Point(SignalAreaCanvas.ActualWidth, polygonPoints[polygonPoints.Count - 1].Y));

            GraphPolyline.Points = polygonPoints;

            SignalAreaCanvas.Children.Clear();
            SignalAreaCanvas.Children.Add(GraphPolyline);            

            void SignalSetting(Color color, double strokeThickness) {
                ColorBrush = new SolidColorBrush();
                ColorBrush.Color = color;

                GraphPolyline = new Polyline();
                GraphPolyline.Stroke = ColorBrush;
                GraphPolyline.StrokeThickness = strokeThickness;

                polygonPoints = new PointCollection();
            }
        }
        private void ViewSimulationRadialChart(List<(double amplitude, double phase)> signalComponentsParameters) {

            if (RadialChartCanvas.ActualWidth > 0) { 
                SolidColorBrush ColorBrush;
                Polyline GraphPolyline;
                PointCollection polygonPoints;

                //List<Ellipse> ellipses = new List<Ellipse>();

                Point point;

                if (centerOfRadialChartCanvasInWidth != RadialChartCanvas.ActualWidth / 2 || centerOfRadialChartCanvasInHeight != RadialChartCanvas.ActualHeight / 2) {
                    centerOfRadialChartCanvasInWidth = RadialChartCanvas.ActualWidth / 2;
                    centerOfRadialChartCanvasInHeight = RadialChartCanvas.ActualHeight / 2;
                }                

                SignalSetting();

                polygonPoints.Add(new Point(centerOfRadialChartCanvasInWidth, centerOfRadialChartCanvasInHeight));

                for (int i = 0; i < signalComponentsParameters.Count; i++) {
                    point.X = signalComponentsParameters[i].amplitude * Math.Cos(signalComponentsParameters[i].phase);
                    point.Y = signalComponentsParameters[i].amplitude * Math.Sin(signalComponentsParameters[i].phase);

                    point = new Point((signalMultiplier * point.X) + polygonPoints[i].X, (signalMultiplier * point.Y) + polygonPoints[i].Y);

                    polygonPoints.Add(point);

                    /*
                    ellipses.Add(new Ellipse() { Width = signalComponentsParameters[i].amplitude,
                                                 Height = signalComponentsParameters[i].amplitude,
                                                 Stroke = Brushes.Black,
                                                 StrokeThickness = 2,
                                                 Margin = new Thickness() { Left = point.X + signalComponentsParameters[i].amplitude, 
                                                                           Right = point.X - signalComponentsParameters[i].amplitude, 
                                                                           Top = point.Y + signalComponentsParameters[i].amplitude, 
                                                                           Bottom = point.Y - signalComponentsParameters[i].amplitude
                                                 }
                    });//*/
                }

                polygonPoints.Add(new Point(0, polygonPoints[polygonPoints.Count - 1].Y));

                GraphPolyline.Points = polygonPoints;

                RadialChartCanvas.Children.Clear();
                RadialChartCanvas.Children.Add(GraphPolyline);

                /*
                foreach (Ellipse item in ellipses) {
                    RadialChartCanvas.Children.Add(item);
                }//*/

                if (true) { }

                void SignalSetting() {
                    ColorBrush = new SolidColorBrush();
                    ColorBrush.Color = Colors.Black;

                    GraphPolyline = new Polyline();
                    GraphPolyline.Stroke = ColorBrush;
                    GraphPolyline.StrokeThickness = 1;

                    polygonPoints = new PointCollection();
                }
            } else {
                RadialChartCanvas.Children.Clear();
            }

            
            //ширина поля не менше ніж сума всіх амплітуд складових сигналу ??
        }

    }
}