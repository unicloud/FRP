#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export]
    public partial class MaintainPaymentScheduleView
    {
        public MaintainPaymentScheduleView()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainPaymentScheduleViewVm ViewModel
        {
            get { return DataContext as MaintainPaymentScheduleViewVm; }
            set { DataContext = value; }
        }
    }
}