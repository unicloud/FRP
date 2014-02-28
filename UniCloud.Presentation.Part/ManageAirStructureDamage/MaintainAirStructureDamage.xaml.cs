#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.ManageAirStructureDamage
{
    [Export(typeof(MaintainAirStructureDamage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainAirStructureDamage : UserControl
    {
        public MaintainAirStructureDamage()
        {
            InitializeComponent();
        }
        [Import]
        public MaintainAirStructureDamageVm ViewModel
        {
            get { return DataContext as MaintainAirStructureDamageVm; }
            set { DataContext = value; }
        }
    }
}
