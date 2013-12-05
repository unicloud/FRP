using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(LinkManManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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
