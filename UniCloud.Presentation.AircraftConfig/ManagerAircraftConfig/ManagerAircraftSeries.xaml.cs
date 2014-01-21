#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export]
    public partial class ManagerAircraftSeries 
    {
        public ManagerAircraftSeries()
        {
            InitializeComponent();
        }

        [Import(typeof(ManagerAircraftSeriesVm))]
        public ManagerAircraftSeriesVm ViewModel
        {
            get { return DataContext as ManagerAircraftSeriesVm; }
            set { DataContext = value; }
        }
    }
}
