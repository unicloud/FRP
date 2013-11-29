//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Purchase.Forwarder
{
    [Export(typeof (ForwarderManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ForwarderManager
    {
        public ForwarderManager()
        {
            InitializeComponent();
        }

        [Import]
        public ForwarderManagerVM ViewModel
        {
            get { return DataContext as ForwarderManagerVM; }
            set { DataContext = value; }
        }
    }
}