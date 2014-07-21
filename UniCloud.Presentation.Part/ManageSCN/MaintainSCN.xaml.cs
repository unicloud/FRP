#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export]
    public partial class MaintainScn
    {
        public MaintainScn()
        {
            InitializeComponent();
        }

        [Import(typeof(MaintainScnVm))]
        public MaintainScnVm ViewModel
        {
            get { return DataContext as MaintainScnVm; }
            set { DataContext = value; }
        }
    }
}