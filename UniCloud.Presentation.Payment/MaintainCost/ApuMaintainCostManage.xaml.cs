#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(ApuMaintainCostManage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ApuMaintainCostManage 
    {
        public ApuMaintainCostManage()
        {
            InitializeComponent();
            this.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnSelectionChanged), true);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                ViewModel.SelectedChanged(e.AddedItems[0]);
            }
        }

        [Import]
        public ApuMaintainCostManageVm ViewModel
        {
            get { return DataContext as ApuMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}
