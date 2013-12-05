using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(QuerySupplier))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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
