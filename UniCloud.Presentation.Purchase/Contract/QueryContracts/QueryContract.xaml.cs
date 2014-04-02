#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.QueryContracts
{
     [Export]
    public partial class QueryContract 
    {
        public QueryContract()
        {
            InitializeComponent();
        }

        [Import(typeof(QueryContractVm))]
        public QueryContractVm ViewModel
        {
            get { return DataContext as QueryContractVm; }
            set { DataContext = value; }
        }
    }
}
