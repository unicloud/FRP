#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export]
    public partial class ItemView
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