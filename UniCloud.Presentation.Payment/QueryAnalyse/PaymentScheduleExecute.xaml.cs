using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof(PaymentScheduleExecute))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PaymentScheduleExecute : UserControl
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
