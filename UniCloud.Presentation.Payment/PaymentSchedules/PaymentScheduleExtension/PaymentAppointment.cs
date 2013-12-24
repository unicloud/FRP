#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/21，15:12
// 文件名：PaymentAppointment.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Telerik.Windows.Controls.ScheduleView;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules.PaymentScheduleExtension
{
    /// <summary>
    /// 付款计划Point
    /// </summary>
    public class PaymentAppointment : Appointment
    {
        private decimal _amount;

        /// <summary>
        ///     付款金额
        /// </summary>
        public decimal Amount
        {
            get { return Storage<PaymentAppointment>()._amount; }
            set
            {
                var storage = Storage<PaymentAppointment>();
                if (storage._amount != value)
                {
                    storage._amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }

        public override IAppointment Copy()
        {
            var newAppointment = new PaymentAppointment();
            newAppointment.CopyFrom(this);
            return newAppointment;
        }

        public override void CopyFrom(IAppointment other)
        {
            var paymentAppointment = other as PaymentAppointment;
            if (paymentAppointment != null)
            {
                Amount = paymentAppointment._amount;
            }
            base.CopyFrom(other);
        }
    }
}