#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageAirStructureDamage
{
    [Export]
    public partial class MaintainAirStructureDamage
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