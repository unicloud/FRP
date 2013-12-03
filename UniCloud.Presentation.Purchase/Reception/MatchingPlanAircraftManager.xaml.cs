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

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(MatchingPlanAircraftManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MatchingPlanAircraftManager : UserControl
    {
        public MatchingPlanAircraftManager()
        {
            InitializeComponent();
        }


        [Import]
        public MatchingPlanAircraftManagerVM ViewModel
        {
            get { return DataContext as MatchingPlanAircraftManagerVM; }
            set { DataContext = value; }
        }
    }
}
