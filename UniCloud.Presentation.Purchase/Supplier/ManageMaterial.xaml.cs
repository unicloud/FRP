#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export]
    public partial class ManageMaterial
    {
        public ManageMaterial()
        {
            InitializeComponent();
        }

        [Import(typeof (ManageMaterialVm))]
        public ManageMaterialVm ViewModel
        {
            get { return DataContext as ManageMaterialVm; }
            set { DataContext = value; }
        }
    }
}