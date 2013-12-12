
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class EngineLeaseReceptionManager : UserControl
    {
        public EngineLeaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import(typeof(EngineLeaseReceptionManagerVM))]
        public EngineLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EngineLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }
   
    }
}
