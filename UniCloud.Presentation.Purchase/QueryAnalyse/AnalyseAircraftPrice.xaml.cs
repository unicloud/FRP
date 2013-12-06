using System.Collections.Generic;
using System.ComponentModel.Composition;
using Telerik.Charting;
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

        private void PieChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            foreach (var dataPoint in this.PieChart.Series[0].DataPoints)
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
