#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Guarantees
{
    [Export(typeof (MaintainGuarantee))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainGuarantee
    {
        public MaintainGuarantee()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainGuaranteeVM ViewModel
        {
            get { return DataContext as MaintainGuaranteeVM; }
            set { DataContext = value; }
        }
    }
}