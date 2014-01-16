#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export]
    public partial class ManagerAircraftType 
    {
        public ManagerAircraftType()
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

        [Import(typeof(ManagerAircraftTypeVm))]
        public ManagerAircraftTypeVm ViewModel
        {
            get { return DataContext as ManagerAircraftTypeVm; }
            set { DataContext = value; }
        }
    }
}
