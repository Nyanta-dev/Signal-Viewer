using System.Windows;

using Signal_Viewer.Type.Signal;

namespace Signal_Viewer {
    public partial class MainWindow : Window {
        private const double settingAreaWidth = 250;
        private const double margin = 10;

        private const double sizeTitleWindow = 31;
        private const double sizeBorderWindow = 8;

        private double minGridWidth = 2 * settingAreaWidth;

        private double signalLenght;
        private AggregateSignal aggregateSignal = new AggregateSignal();

        public MainWindow() {
            InitializeComponent();
            CanvasSetting();

            this.MinWidth = this.Width;
            this.MinHeight = this.Height;
        }

        public void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            GridSetting(sender);
            //SignalSetting(sender);
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
        }
        private void SignalSetting(object sender) {
            signalLenght = SignalArea.Width.Value / 10;
        }

        
    }
}