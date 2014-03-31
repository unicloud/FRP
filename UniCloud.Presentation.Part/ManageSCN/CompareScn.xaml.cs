#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(CompareScn))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CompareScn 
    {
        public CompareScn()
        {
            InitializeComponent();
        }
        [Import]
        public CompareScnVm ViewModel
        {
            get { return DataContext as CompareScnVm; }
            set { DataContext = value; }
        }
    }
}
