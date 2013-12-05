using System.ComponentModel.Composition;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace UniCloud.Presentation.Purchase.QueryAnalyse
{
    [Export(typeof(AnalyseAircraftPrice))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AnalyseAircraftPrice
    {
        public AnalyseAircraftPrice()
        {
            InitializeComponent();
        }

        [Import]
        public AnalyseAircraftPriceVm ViewModel
        {
            get { return DataContext as AnalyseAircraftPriceVm; }
            set { DataContext = value; }
        }

        private void TotalChartTrackBallBehaviorTrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                var data = closestDataPoint.DataPoint.DataItem as FinancialData;
                if (data != null)
                {
                    Date.Text = data.Date.ToString("MMM dd, yyyy");
                    Price.Text = data.Volume.ToString("0,0.00");
                }
            }
        }

    }
}
