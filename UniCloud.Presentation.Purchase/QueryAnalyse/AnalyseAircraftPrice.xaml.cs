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
                    Price.Text = data.Close.ToString("0,0.00");
                }
            }
        }

        private void ImportTypeChartTrackBallBehaviorTrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                var data = closestDataPoint.DataPoint.DataItem as FinancialData;
                if (data != null)
                {
                    ImportDate.Text = data.Date.ToString("MMM dd, yyyy");
                    PurchasePrice.Text = data.Close.ToString("0,0.00");
                    LeasePrice.Text = data.Close.ToString("0,0.00");
                }
            }
        }
    }
}
