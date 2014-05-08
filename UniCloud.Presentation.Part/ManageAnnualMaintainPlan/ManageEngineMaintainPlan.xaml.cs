#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.ManageAnnualMaintainPlan
{
    [Export(typeof(ManageEngineMaintainPlan))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageEngineMaintainPlan
    {
        public ManageEngineMaintainPlan()
        {
            InitializeComponent();
        }
        [Import]
        public ManageEngineMaintainPlanVm ViewModel
        {
            get { return DataContext as ManageEngineMaintainPlanVm; }
            set { DataContext = value; }
        }

        private void RadPaneGroupSelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (sender is RadPaneGroup)
            {
                var group = sender as RadPaneGroup;
                ViewModel.PaneSelectedChange(group.SelectedPane.Name.Equals("NonFhaPane") ? 0 : 1);
            }
        }
    }
}
