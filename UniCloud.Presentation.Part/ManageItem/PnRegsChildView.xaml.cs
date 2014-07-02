#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export]
    public partial class PnRegsChildView
    {
        public PnRegsChildView()
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