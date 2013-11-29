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
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(EngineLeaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EngineLeaseReceptionManager : UserControl
    {
        public EngineLeaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import]
        public EngineLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EngineLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}
