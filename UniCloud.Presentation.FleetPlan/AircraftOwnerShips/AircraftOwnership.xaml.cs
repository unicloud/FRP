#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.AircraftOwnerShips
{
    [Export(typeof(AircraftOwnership))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftOwnership
    {
        public AircraftOwnership()
        {
            InitializeComponent();
        }

        [Import]
        public AircraftOwnershipVM ViewModel
        {
            get { return DataContext as AircraftOwnershipVM; }
            set { DataContext = value; }
        }
    }
}