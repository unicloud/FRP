#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (QueryContract))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryContract
    {
        public QueryContract()
        {
            InitializeComponent();
        }

        [Import(typeof (QueryContractVM))]
        public QueryContractVM ViewModel
        {
            get { return DataContext as QueryContractVM; }
            set { DataContext = value; }
        }

        
    }
}