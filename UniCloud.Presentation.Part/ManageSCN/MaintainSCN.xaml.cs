#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainSCN))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainSCN 
    {
        public MaintainSCN()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainSCNVm ViewModel
        {
            get { return DataContext as MaintainSCNVm; }
            set { DataContext = value; }
        }
    }
}
