#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export]
    public partial class CompareScn
    {
        public CompareScn()
        {
            InitializeComponent();
        }

        [Import(typeof(CompareScnVm))]
        public CompareScnVm ViewModel
        {
            get { return DataContext as CompareScnVm; }
            set { DataContext = value; }
        }
    }
}