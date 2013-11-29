using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace UniCloud.Presentation.Purchase.QueryAnalyse
{
    [Export(typeof(AnalyseAircraftPrice))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AnalyseAircraftPrice : UserControl
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

        private void TotalChartTrackBallBehavior_TrackInfoUpdated(object sender, Telerik.Windows.Controls.ChartView.TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                var data = closestDataPoint.DataPoint.DataItem as FinancialData;
                date.Text = data.Date.ToString("MMM dd, yyyy");
                price.Text = data.Close.ToString("0,0.00");
            }
        }

        private void ImportTypeChartTrackBallBehavior_TrackInfoUpdated(object sender, Telerik.Windows.Controls.ChartView.TrackBallInfoEventArgs e)
        {
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                var data = closestDataPoint.DataPoint.DataItem as FinancialData;
                ImportDate.Text = data.Date.ToString("MMM dd, yyyy");
                PurchasePrice.Text = data.Close.ToString("0,0.00");
                LeasePrice.Text = data.Close.ToString("0,0.00");
            }
        }
    }
}
