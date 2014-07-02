#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export]
    public partial class PnRegView
    {
        public PnRegView()
        {
            InitializeComponent();
        }

        [Import]
        public PnRegVm ViewModel
        {
            get { return DataContext as PnRegVm; }
            set { DataContext = value; }
        }
    }
}