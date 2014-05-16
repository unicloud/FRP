#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(NonFhaMaintainCostManage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class NonFhaMaintainCostManage 
    {
        public NonFhaMaintainCostManage()
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
        public NonFhaMaintainCostManageVm ViewModel
        {
            get { return DataContext as NonFhaMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}
