#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export]
    public partial class LinkManManager
    {
        public LinkManManager()
        {
            InitializeComponent();
        }

        [Import]
        public LinkManManagerVM ViewModel
        {
            get { return DataContext as LinkManManagerVM; }
            set { DataContext = value; }
        }
    }
}