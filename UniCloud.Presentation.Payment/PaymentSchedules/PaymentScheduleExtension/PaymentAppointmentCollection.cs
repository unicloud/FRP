#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/21，15:12
// 文件名：PaymentAppointmentCollection.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.ObjectModel;
using System.Linq;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules.PaymentScheduleExtension
{
    /// <summary>
    ///     付款计划Appointment集合
    /// </summary>
    public class PaymentAppointmentCollection : ObservableCollection<PaymentAppointment>
    {
        /// <summary>
        ///     付款计划转化为Appointment同时，添加到集合中
        /// </summary>
        /// <param name="paymentScheduleLines"></param>
        public void ConvertAppointmentAndAdd(ObservableCollection<PaymentScheduleLineDTO> paymentScheduleLines)
        {
            paymentScheduleLines.ToList().ForEach(p =>
            {
                var paymentAppoint = AppointmentConvertHelper.ConvertToAppointPayment(p);
                Add(paymentAppoint);
            });
        }
    }
}