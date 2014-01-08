//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Portal.Manager
{
    [Export(typeof(ManagerPortal))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManagerPortal
    {
        public ManagerPortal()
        {
            InitializeComponent();
        }

        [Import]
        public ManagerPortalVm ViewModel
        {
            get { return DataContext as ManagerPortalVm; }
            set { DataContext = value; }
        }
    }
}
