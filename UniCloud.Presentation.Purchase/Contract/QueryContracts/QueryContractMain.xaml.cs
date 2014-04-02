#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.QueryContracts
{
     [Export]
    public partial class QueryContractMain 
    {
        public QueryContractMain()
        {
            InitializeComponent();
        }

        [Import(typeof(QueryContractMainVm))]
        public QueryContractMainVm ViewModel
        {
            get { return DataContext as QueryContractMainVm; }
            set { DataContext = value; }
        }
    }
}
