﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(MaintainPaymentSchedule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MaintainPaymentSchedule 
    {
        public MaintainPaymentSchedule()
        {
            InitializeComponent();
        }
        [Import]
        public MaintainPaymentScheduleVm ViewModel
        {
            get { return DataContext as MaintainPaymentScheduleVm; }
            set { DataContext = value; }
        }
    }
}