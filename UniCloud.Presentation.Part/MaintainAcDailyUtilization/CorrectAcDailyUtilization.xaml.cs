#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.MaintainAcDailyUtilization
{
    [Export(typeof(CorrectAcDailyUtilization))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CorrectAcDailyUtilization : UserControl
    {
        public CorrectAcDailyUtilization()
        {
            InitializeComponent();
        }
        [Import]
        public CorrectAcDailyUtilizationVm ViewModel
        {
            get { return DataContext as CorrectAcDailyUtilizationVm; }
            set { DataContext = value; }
        }
    }
}
