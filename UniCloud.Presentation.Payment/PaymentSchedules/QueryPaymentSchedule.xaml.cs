using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(QueryPaymentSchedule))]
    [PartCreationPolicy(CreationPolicy.Shared)]

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
