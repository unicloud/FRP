#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls.ColorEditor;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export]
    public partial class ManageSystemConfig
    {
        public ManageSystemConfig()
        {
            InitializeComponent();
        }

        [Import]
        public ManageSystemConfigVm ViewModel
        {
            get { return DataContext as ManageSystemConfigVm; }
            set { DataContext = value; }
        }

        private void ColorEditorSelectedColorChanged(object sender, ColorChangeEventArgs e)
        {
            ViewModel.ColorEditorSelectedColorChanged(sender, e);
        }
    }
}