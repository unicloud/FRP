#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (SupplierMaterialManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SupplierMaterialManager
    {
        public SupplierMaterialManager()
        {
            InitializeComponent();
        }
        [Import]
        public SupplierMaterialManagerVM ViewModel
        {
            get { return DataContext as SupplierMaterialManagerVM; }
            set { DataContext = value; }
        }

       
    }
}