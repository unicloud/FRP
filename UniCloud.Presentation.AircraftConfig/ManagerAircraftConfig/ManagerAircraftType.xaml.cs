#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export]
    public partial class ManagerAircraftType 
    {
        public ManagerAircraftType()
        {
            InitializeComponent();
        }


        [Import(typeof(ManagerAircraftTypeVm))]
        public ManagerAircraftTypeVm ViewModel
        {
            get { return DataContext as ManagerAircraftTypeVm; }
            set { DataContext = value; }
        }
    }
}
