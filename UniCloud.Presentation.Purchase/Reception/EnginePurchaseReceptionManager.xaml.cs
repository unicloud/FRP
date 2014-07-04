#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class EnginePurchaseReceptionManager
    {
        public EnginePurchaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import(typeof (EnginePurchaseReceptionManagerVM))]
        public EnginePurchaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EnginePurchaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}