using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof(FinancingDemandForecast))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class FinancingDemandForecast
    {
        public FinancingDemandForecast()
        {
            InitializeComponent();
        }
        [Import]
        public FinancingDemandForecastVM ViewModel
        {
            get { return DataContext as FinancingDemandForecastVM; }
            set { DataContext = value; }
        }
    }
}
