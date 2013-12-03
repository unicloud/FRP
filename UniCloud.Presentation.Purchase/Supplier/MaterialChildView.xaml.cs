#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(MaterialChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaterialChildView
    {
        public MaterialChildView()
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