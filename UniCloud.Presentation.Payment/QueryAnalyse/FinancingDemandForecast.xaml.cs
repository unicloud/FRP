﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

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
