#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export]
    public partial class SnRegView
    {
        public SnRegView()
        {
            InitializeComponent();
        }

        [Import]
        public SnRegVm ViewModel
        {
            get { return DataContext as SnRegVm; }
            set { DataContext = value; }
        }
    }
}