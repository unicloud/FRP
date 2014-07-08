#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class EngineLeaseReceptionManager
    {
        public EngineLeaseReceptionManager()
        {
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
            InitializeComponent();
        }

        [Import(typeof (EngineLeaseReceptionManagerVM))]
        public EngineLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as EngineLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}