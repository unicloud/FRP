#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class AircraftPurchaseReceptionManager
    {
        public AircraftPurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import(typeof (AircraftPurchaseReceptionManagerVM))]
        public AircraftPurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftPurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}