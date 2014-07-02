﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class PassengerAircraftTrend
    {
        public PassengerAircraftTrend()
        {
            InitializeComponent();
        }

        [Import]
        public PassengerAircraftTrendVm ViewModel
        {
            get { return DataContext as PassengerAircraftTrendVm; }
            set { DataContext = value; }
        }
    }
}