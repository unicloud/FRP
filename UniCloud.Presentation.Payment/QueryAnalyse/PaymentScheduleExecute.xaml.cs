#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export]
    public partial class PaymentScheduleExecute
    {
        public PaymentScheduleExecute()
        {
            InitializeComponent();
        }

        [Import]
        public PaymentScheduleExecuteVm ViewModel
        {
            get { return DataContext as PaymentScheduleExecuteVm; }
            set { DataContext = value; }
        }
    }
}