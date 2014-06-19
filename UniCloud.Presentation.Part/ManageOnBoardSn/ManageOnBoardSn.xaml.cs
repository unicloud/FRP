using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.ManageOnBoardSn
{
    [Export(typeof (ManageOnBoardSn))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageOnBoardSn : UserControl
    {
        public ManageOnBoardSn()
        {
            InitializeComponent();
        }

        [Import]
        public ManageOnBoardSnVm ViewModel
        {
            get { return DataContext as ManageOnBoardSnVm; }
            set { DataContext = value; }
        }
    }
}