using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PriceOutlierLib;

namespace PriceOutlier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OutlierFile obj = new OutlierFile();
        public ChartValues<DateTimePoint> CleanPoints { get; set; }
        public ChartValues<DateTimePoint> OutlierPoints { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            CleanPoints = new ChartValues<DateTimePoint>();
            OutlierPoints = new ChartValues<DateTimePoint>();
            DataContext = this;
        }

        private void LoadDataPoints(object sender, RoutedEventArgs e)
        {
            obj.ReadData();

            // this chart only supports double (bad)
            var cds = obj.CleanData;
            foreach (var cd in cds) {
                CleanPoints.Add(new DateTimePoint(cd.Key, Convert.ToDouble(cd.Value)));
            }

            var ods = obj.OutlierData;
            foreach (var od in ods) {
                OutlierPoints.Add(new DateTimePoint(od.Key, Convert.ToDouble(od.Value)));
            }
            loadBtn.IsEnabled = false;  // disable the button once loaded
        }

        private void ShowHideOutlier(object sender, RoutedEventArgs e)
        {
            if (outShow.IsChecked == true)
                outSeries.Visibility = Visibility.Hidden;
            else
                outSeries.Visibility = Visibility.Visible;
        }
    }
}
