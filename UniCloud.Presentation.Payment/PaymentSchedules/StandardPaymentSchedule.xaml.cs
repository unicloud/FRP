#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export]
    public partial class StandardPaymentSchedule
    {
        public StandardPaymentSchedule()
        {
            InitializeComponent();
        }

        [Import]
        public StandardPaymentScheduleVM ViewModel
        {
            get { return DataContext as StandardPaymentScheduleVM; }
            set { DataContext = value; }
        }
    }
}