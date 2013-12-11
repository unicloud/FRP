using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class EnginePurchaseReceptionManager : UserControl
    {
        public EnginePurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import(typeof(EnginePurchaseReceptionManagerVM))]
        public EnginePurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EnginePurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}
