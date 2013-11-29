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
    [Export(typeof(EnginePurchaseReceptionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EnginePurchaseReceptionManager : UserControl
    {
        public EnginePurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import]
        public EnginePurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EnginePurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}
