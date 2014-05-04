#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(MaintainCreditNoteManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainCreditNoteManager 
    {
        public MaintainCreditNoteManager()
        {
            InitializeComponent();
        }
        [Import]
        public MaintainCreditNoteManagerVm ViewModel
        {
            get { return DataContext as MaintainCreditNoteManagerVm; }
            set { DataContext = value; }
        }
    }
}
