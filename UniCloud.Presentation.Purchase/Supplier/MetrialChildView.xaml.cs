#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(MetrialChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MetrialChildView
    {
        public MetrialChildView()
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