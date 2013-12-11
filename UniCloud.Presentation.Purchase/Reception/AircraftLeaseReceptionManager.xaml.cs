
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Documents.Fixed.Model;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(AircraftLeaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftLeaseReceptionManager : UserControl
    {
        public AircraftLeaseReceptionManager()
        {
            InitializeComponent();
        }
        [Import]
        public AircraftLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }

    }
}
