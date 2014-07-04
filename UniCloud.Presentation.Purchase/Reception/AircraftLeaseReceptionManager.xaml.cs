#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class AircraftLeaseReceptionManager
    {
        public AircraftLeaseReceptionManager()
        {
            InitializeComponent();
        }

        [Import(typeof (AircraftLeaseReceptionManagerVM))]
        public AircraftLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }
    }
}