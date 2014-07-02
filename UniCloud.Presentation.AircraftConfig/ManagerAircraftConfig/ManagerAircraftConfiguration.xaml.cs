#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export]
    public partial class ManagerAircraftConfiguration
    {
        public ManagerAircraftConfiguration()
        {
            InitializeComponent();
            this.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnSelectionChanged), true);
        }

        [Import(typeof (ManagerAircraftConfigurationVm))]
        public ManagerAircraftConfigurationVm ViewModel
        {
            get { return DataContext as ManagerAircraftConfigurationVm; }
            set { DataContext = value; }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                ViewModel.SelectedChanged(e.AddedItems[0]);
            }
        }
    }
}