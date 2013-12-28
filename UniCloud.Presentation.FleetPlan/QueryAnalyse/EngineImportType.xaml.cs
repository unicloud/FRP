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
    [Export(typeof(EngineImportType))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EngineImportType : UserControl
    {
        public EngineImportType()
        {
            InitializeComponent();
        }
        [Import]
        public EngineImportTypeVm ViewModel
        {
            get { return DataContext as EngineImportTypeVm; }
            set { DataContext = value; }
        }
    }
}
