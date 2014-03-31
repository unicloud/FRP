#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof(AddDocumetChild))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AddDocumetChild
    {
        public AddDocumetChild()
        {
            InitializeComponent();
        }

        [Import]
        public QueryContractVM ViewModel
        {
            get { return DataContext as QueryContractVM; }
            set { DataContext = value; }
        }
    }
}