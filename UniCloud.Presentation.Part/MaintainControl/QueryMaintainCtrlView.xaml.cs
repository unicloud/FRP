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

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (ManageRemovalAndInstallationView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryMaintainCtrlView : UserControl
    {
        public QueryMaintainCtrlView()
        {
            InitializeComponent();
        }

        [Import]
        public ManageRemovalAndInstallationVm ViewModel
        {
            get { return DataContext as ManageRemovalAndInstallationVm; }
            set { DataContext = value; }
        }
    }
}
