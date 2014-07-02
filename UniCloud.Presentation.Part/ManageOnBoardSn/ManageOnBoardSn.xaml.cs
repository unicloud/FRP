#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageOnBoardSn
{
    [Export]
    public partial class ManageOnBoardSn
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