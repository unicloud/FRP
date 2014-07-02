#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManageSubsidiary
{
    [Export]
    public partial class BranchCompany
    {
        public BranchCompany()
        {
            InitializeComponent();
        }

        [Import]
        public BranchCompanyVm ViewModel
        {
            get { return DataContext as BranchCompanyVm; }
            set { DataContext = value; }
        }
    }
}