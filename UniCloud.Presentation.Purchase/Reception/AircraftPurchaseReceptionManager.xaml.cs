
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class AircraftPurchaseReceptionManager : UserControl
    {
        public AircraftPurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }
        [Import(typeof(AircraftPurchaseReceptionManagerVM))]
        public AircraftPurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftPurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }

    }
}
