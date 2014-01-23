
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class AircraftLeaseReceptionManager : UserControl
    {
        public AircraftLeaseReceptionManager()
        {
            InitializeComponent();
        }
        [Import(typeof(AircraftLeaseReceptionManagerVM))]
        public AircraftLeaseReceptionManagerVM ViewModel
        {
            get { return DataContext as AircraftLeaseReceptionManagerVM; }
            set { DataContext = value; }
        }

    }
}
