#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainSCN))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainSCN 
    {
        public MaintainSCN()
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
        public MaintainSCNVm ViewModel
        {
            get { return DataContext as MaintainSCNVm; }
            set { DataContext = value; }
        }
    }
}
