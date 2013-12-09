using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace UniCloud.Presentation.Purchase.QueryAnalyse
{
    [Export(typeof(AnalyseAircraftPrice))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AnalyseAircraftPrice
    {
        private List<FinancialData> _purchaseImportType;
        private List<FinancialData> _leaseImportType;
        public AnalyseAircraftPrice()
        {
            InitializeComponent();
        }

        [Import]
        public AnalyseAircraftPriceVm ViewModel
        {
            get
            {
                return DataContext as AnalyseAircraftPriceVm;
            }
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

        private void ImportTypeChartTrackBallBehaviorTrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            _purchaseImportType = ImportTypeBarSeries.Series[0].ItemsSource as List<FinancialData>;
            _leaseImportType = ImportTypeBarSeries.Series[1].ItemsSource as List<FinancialData>;
            DataPointInfo closestDataPoint = e.Context.ClosestDataPoint;
            if (closestDataPoint != null)
            {
                var data = closestDataPoint.DataPoint.DataItem as FinancialData;
                if (data != null)
                {
                    UpdateImportTypeData(data);
                }
            }
        }

        private void UpdateImportTypeData(FinancialData data)
        {
            YearTextBlock.Text = " " + data.Date.ToShortDateString();
            var purchase = _purchaseImportType.FirstOrDefault(p => p.Date.ToShortDateString() == data.Date.ToShortDateString());
            var lease = _leaseImportType.FirstOrDefault(p => p.Date.ToShortDateString() == data.Date.ToShortDateString());
            if (purchase != null && lease != null)
            {
                var tempPercent = purchase.Volume * 100 / (purchase.Volume + lease.Volume);
                ImportTypePieChart.Series[0].DataPoints[0].Value = purchase.Volume;
                ImportTypePieChart.Series[0].DataPoints[0].Label = "购买 " + tempPercent + "%";
                ImportTypePieChart.Series[0].DataPoints[1].Value = lease.Volume;
                ImportTypePieChart.Series[0].DataPoints[1].Label = "租赁 " + (100 - tempPercent) + "%";
            }
        }
        private void PieChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            foreach (var dataPoint in this.ImportTypePieChart.Series[0].DataPoints)
            {
                //string countryName = ((CountryData)dataPoint.DataItem).Name;
                //if (!this.SelectableCountries.Contains(countryName))
                //{
                //    dataPoint.IsSelected = false;
                //}
            }

            this.UpdateAll(this.PieChart.Series[0].DataPoints);
        }

        private void UpdateAll(IEnumerable<DataPoint> dataPoints)
        {
            //foreach (var dataPoint in dataPoints)
            //{
            //    string countryName = ((CountryData)dataPoint.DataItem).Name;
            //    this.UpdatePieSlice(countryName, dataPoint.IsSelected);
            //    this.UpdateBar(countryName, dataPoint.IsSelected);
            //    this.UpdateMapShape(countryName, dataPoint.IsSelected);
            //    this.UpdateLine(countryName, dataPoint.IsSelected);
            //}
        }

    }
}
