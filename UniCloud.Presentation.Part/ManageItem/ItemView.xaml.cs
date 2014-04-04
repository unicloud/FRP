using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof (ItemView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();
        }

        [Import]
        public ItemVm ViewModel
        {
            get { return DataContext as ItemVm; }
            set { DataContext = value; }
        }
    }
}