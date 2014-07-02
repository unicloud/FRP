#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.ManageContracts
{
    [Export(typeof (AddDocumetChild))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AddDocumetChild
    {
        public AddDocumetChild()
        {
            InitializeComponent();
        }

        [Import]
        public ManageContractVm ViewModel
        {
            get { return DataContext as ManageContractVm; }
            set { DataContext = value; }
        }
    }
}