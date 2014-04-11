#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof(ManageSystemConfig))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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

        private void ColorEditorSelectedColorChanged(object sender, Telerik.Windows.Controls.ColorEditor.ColorChangeEventArgs e)
        {
            ViewModel.ColorEditorSelectedColorChanged(sender, e);
        }

    }
}
