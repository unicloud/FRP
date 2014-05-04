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

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof(ManageIndexAircraftView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageIndexAircraftView
    {
        public ManageIndexAircraftView()
        {
            InitializeComponent();
        }

        [Import]
        public ManageIndexAircraftVM ViewModel
        {
            get { return DataContext as ManageIndexAircraftVM; }
            set { DataContext = value; }
        }

     
    }
}
