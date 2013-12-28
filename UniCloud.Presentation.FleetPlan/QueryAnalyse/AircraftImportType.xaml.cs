using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(AircraftImportType))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftImportType : UserControl
    {
        public AircraftImportType()
        {
            InitializeComponent();
        }
        [Import]
        public AircraftImportTypeVm ViewModel
        {
            get { return DataContext as AircraftImportTypeVm; }
            set { DataContext = value; }
        }
    }
}
