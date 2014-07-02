#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export]
    public partial class QuerySupplier
    {
        public QuerySupplier()
        {
            InitializeComponent();
        }

        [Import]
        public QuerySupplierVM ViewModel
        {
            get { return DataContext as QuerySupplierVM; }
            set { DataContext = value; }
        }
    }
}