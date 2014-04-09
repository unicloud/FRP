using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof (ItemControlView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ItemControlView : UserControl
    {
        public ItemControlView()
        {
            InitializeComponent();
        }

        [Import]
        public ItemControlVm ViewModel
        {
            get { return DataContext as ItemControlVm; }
            set { DataContext = value; }
        }
    }
}