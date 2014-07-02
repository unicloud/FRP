#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export]
    public partial class QueryPaymentSchedule
    {
        public QueryPaymentSchedule()
        {
            InitializeComponent();
        }

        [Import]
        public QueryPaymentScheduleVM ViewModel
        {
            get { return DataContext as QueryPaymentScheduleVM; }
            set { DataContext = value; }
        }
    }
}