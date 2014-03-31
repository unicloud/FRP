#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainScn))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainScn 
    {
        public MaintainScn()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainScnVm ViewModel
        {
            get { return DataContext as MaintainScnVm; }
            set { DataContext = value; }
        }
    }
}
